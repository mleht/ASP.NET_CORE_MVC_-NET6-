using System.ComponentModel.DataAnnotations;

namespace Core_NET6.Models
{
    public class Movie
    {
        [Key]  // primary key
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
