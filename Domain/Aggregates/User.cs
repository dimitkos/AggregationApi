namespace Domain.Aggregates
{
    public class User
    {
        public long Id { get; }
        public string Email { get; }
        public string Password { get; }

        public User(long id, string email, string password)
        {
            Id = id;
            Email = email;
            Password = password;
        }

        public static User CreateNew(long id, string email, string password)
        {
            return new User(id, email, password);
        }
    }
}
