using Api.Middlewares;
using Application;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common;
using FluentValidation;
using Gateway;
using IdGen.DependencyInjection;
using Infrastructure;
using Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Polly;
using Serilog;
using System.Reflection;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host
                .UseSerilog((context, configuration) =>
                        configuration.ReadFrom.Configuration(context.Configuration))
                .UseServiceProviderFactory<ContainerBuilder>(new AutofacServiceProviderFactory())
                    .ConfigureContainer((Action<ContainerBuilder>)(builder =>
                    {
                        RegisterAutofacModules(builder);
                    }));

            var serilogLogger = new LoggerConfiguration()
                    .CreateLogger();

            serilogLogger.Information("");

            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            Configure(app);
        }

        private static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddControllers();

            services.AddHttpClient(Constants.HttpClients.Comments)
                .AddTransientHttpErrorPolicy(policyBuilder =>
                    policyBuilder.WaitAndRetryAsync(3, retryNumber => TimeSpan.FromMilliseconds(600)))
                .AddTransientHttpErrorPolicy(policyBuilder =>
                    policyBuilder.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient(Constants.HttpClients.Recipes)
                .AddTransientHttpErrorPolicy(policyBuilder =>
                    policyBuilder.WaitAndRetryAsync(3, retryNumber => TimeSpan.FromMilliseconds(600)))
                .AddTransientHttpErrorPolicy(policyBuilder =>
                    policyBuilder.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(AutofacApplicationModule)));

            var apiInstanceSettings = configuration.GetSection(nameof(ApiInstanceSettings)).Get<ApiInstanceSettings>();
            services.AddIdGen(apiInstanceSettings.IdConfiguration);

            services.AddAuthorization();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            RegisterConfiguration(services, configuration);
            RegisterDatabase(services, configuration);
            RegisterCaching(services);
        }


        private static void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseMiddleware<ExceptionsMiddleware>();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }

        private static void RegisterConfiguration(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddOptions<ApiInstanceSettings>().Bind(configuration.GetSection(nameof(ApiInstanceSettings))).ValidateDataAnnotations();
            services.AddOptions<CacheSettings>().Bind(configuration.GetSection(nameof(CacheSettings))).ValidateDataAnnotations();
            services.AddOptions<ApiConfiguration>().Bind(configuration.GetSection(nameof(ApiConfiguration))).ValidateDataAnnotations();
        }

        private static void RegisterAutofacModules(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacApiModule());
            builder.RegisterModule(new AutofacApplicationModule());
            builder.RegisterModule(new AutofacCommonModule());
            builder.RegisterModule(new AutofacInfrastructureModule());
            builder.RegisterModule(new AutofacGatewayModule());
        }

        private static void RegisterDatabase(IServiceCollection services, ConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString(Constants.Databases.Aggregation);

            services
                .AddDbContext<AggreegationDbContext>(options => options.UseSqlite(connectionString), ServiceLifetime.Transient);
        }

        private static void RegisterCaching(IServiceCollection services)
        {
            services.AddMemoryCache();
        }
    }
}