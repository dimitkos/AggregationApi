using IdGen;

namespace Common
{
    public interface IIdGenerator
    {
        long GenerateId();
    }

    class UniqueIdGenerator : IIdGenerator
    {
        private readonly IdGenerator _idGenerator;

        public UniqueIdGenerator(IdGenerator idGenerator)
        {
            _idGenerator = idGenerator;
        }

        public long GenerateId()
            => _idGenerator.CreateId();
    }
}
