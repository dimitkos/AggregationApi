using MediatR;
using Shared;

namespace Application.Commands.Users
{
    public class Login : IRequest<string>
    {
        public LoginPayload Payload { get; }

        public Login(LoginPayload payload)
        {
            Payload = payload;
        }
    }
}
