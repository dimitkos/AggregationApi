using Application.Services;
using Application.Services.Infrastructure;
using Common;
using Domain.Aggregates;
using MediatR;

namespace Application.Commands.Users
{
    class AddUserHandler : IRequestHandler<AddUser, Unit>
    {
        private readonly IUserPersistence _persistence;
        private readonly IPasswordHasher _hasher;
        private readonly IIdGenerator _idGenerator;

        public AddUserHandler(
            IUserPersistence persistence,
            IPasswordHasher hasher,
            IIdGenerator idGenerator)
        {
            _persistence = persistence;
            _hasher = hasher;
            _idGenerator = idGenerator;
        }

        public async Task<Unit> Handle(AddUser request, CancellationToken cancellationToken)
        {
            var hash = _hasher.Hash(request.Payload.Password);

            var user = new User(
                id: _idGenerator.GenerateId(),
                email: request.Payload.Email,
                password: hash);

            await _persistence.AddUser(user);

            return Unit.Value;
        }
    }
}
