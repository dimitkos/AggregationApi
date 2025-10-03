using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class ApiInstanceSettings
    {
        [Required]
        public int IdConfiguration { get; set; }
    }

    public class CacheSettings
    {
        [Required]
        public int AbsoluteExpiration { get; set; }
        [Required]
        public int SlidingExpiration { get; set; }
    }

    public class StatisticsPerformanceConfigurationJob
    {
        [Required]
        public int MinutesToCalculate { get; set; }
        [Required]
        public double ThresholdMultiplier { get; set; }
        [Required]
        public string CronExpression { get; set; }
    }

    public class JwtSettings
    {
        [Required]
        public string Secret { get; set; }
        [Required]
        public string Issuer { get; set; }
        [Required]
        public string Audience { get; set; }
        [Required]
        public int ExpirationInMinutes { get; set; }
    }
}
