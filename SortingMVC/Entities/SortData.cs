using System.ComponentModel.DataAnnotations;

namespace SortingMVC.Entities
{
    public class SortData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public DateTime DateJoined { get; set; }
    }
}
