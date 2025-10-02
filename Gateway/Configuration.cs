using System.ComponentModel.DataAnnotations;

namespace Gateway
{
    public class ApiConfiguration
    {
        [Required]
        public string CommentsUrl { get; set; }
        [Required]
        public string RecipesUrl { get; set; }
    }
}
