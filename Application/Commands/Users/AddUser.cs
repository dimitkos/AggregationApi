using MediatR;
using Shared;

namespace Application.Commands.Users
{
    public class AddUser : IRequest<Unit>
    {
        public AddUserPayload Payload { get; }

        public AddUser(AddUserPayload payload)
        {
            Payload = payload;
        }
    }
}
