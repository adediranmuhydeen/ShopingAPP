using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Entities.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string Description { get; set; }=string.Empty;
        [Required]
        public string ImageURL { get; set; } = string.Empty;
        [Required]
        public decimal? Price { get; set; }
        [Required]
        public int Qtr { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }= string.Empty;

    }
}
