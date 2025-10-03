namespace Shared
{
    public class AddUserPayload
    {
        public string Email { get; }
        public string Password { get; }

        public AddUserPayload(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }

    public class LoginPayload
    {
        public string Email { get; }
        public string Password { get; }
        public LoginPayload(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
