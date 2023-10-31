using System.ComponentModel.DataAnnotations;

namespace CrudUsingADO.Models
{
    public class Users
    {
        public int Id { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}
