using System.ComponentModel.DataAnnotations;

namespace CrudUsingADO.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public int? Salary { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public int? Did { get; set; }

        [Required]
        [ScaffoldColumn(false)] //Don't want to create filed
        public string? Dname { get; set; }

        [ScaffoldColumn(false)]
        public int IsActive { get; set; }



    }
}
