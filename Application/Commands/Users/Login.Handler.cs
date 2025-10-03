using Application.Services;
using Application.Services.Infrastructure;
using Domain.Aggregates;
using MediatR;

namespace Application.Commands.Users
{
    public class LoginHandler : IRequestHandler<Login, string>
    {
        private readonly IEntityRetrieval<string, User> _retrieval;
        private readonly IPasswordHasher _hasher;
        private readonly ITokenProvider _tokenProvider;

        public LoginHandler(
            IEntityRetrieval<string, User> retrieval,
            IPasswordHasher hasher,
            ITokenProvider tokenProvider)
        {
            _retrieval = retrieval;
            _hasher = hasher;
            _tokenProvider = tokenProvider;
        }

        public async Task<string> Handle(Login request, CancellationToken cancellationToken)
        {
            var user = await _retrieval.TryRetrieve(request.Payload.Email);

            if (user is null)
                throw new Exception("User does not exist");

            var verified = _hasher.Verify(request.Payload.Password, user.Password);

            if(!verified)
                throw new Exception("Invalid credentials");

            var token = _tokenProvider.Create(user);

            return token;
        }
    }
}
