using System.ComponentModel.DataAnnotations;

namespace CrudUsingADO.Models
{
    public class Product
    {
        public int pid { get; set; }
        [Required]
        public string? pname { get; set; }
        [Required]
        public int price { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public int? cid { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public string? cname { get; set; }

        [ScaffoldColumn(false)]
        public int IsActive { get; set; }


    }
}
