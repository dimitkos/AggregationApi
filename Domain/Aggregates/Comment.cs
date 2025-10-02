namespace Domain.Aggregates
{
    public class Comment
    {
        public int Id { get; }
        public int PostId { get; }
        public string Name { get; }
        public string Email { get; }
        public string Body { get;}

        public Comment(int id, int postId, string name, string email, string body)
        {
            Id = id;
            PostId = postId;
            Name = name;
            Email = email;
            Body = body;
        }
    }
}
