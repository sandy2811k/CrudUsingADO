using System.ComponentModel.DataAnnotations;

namespace CrudUsingADO.Models
{
    public class Course
    {
        public int id { get; set; }
        [Required]
        public string? coursename { get; set; }

        [Required]
        public string? duration { get; set; }

        [Required]
        public int? fees { get; set; }
    }
}
