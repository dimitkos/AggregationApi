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
}
