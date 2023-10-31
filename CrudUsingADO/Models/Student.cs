using System.ComponentModel.DataAnnotations;

namespace CrudUsingADO.Models
{
    public class Student
    {
        public int RollNo { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? city { get; set; }

        [Required]
        public int percentage { get; set; }
    }
}
