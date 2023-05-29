using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaMiaPizzeria.Models
{
    public class PizzaModel
    {
        public int id { get; set; }
        
        [MaxLength(40)]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string? Name { get; set; }
        
        [MaxLength(100)]
        [Column(TypeName = "text")]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string? Description { get; set; }
        
        [MaxLength(300)]
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Url]
        public string? ImgSource { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Range(10, 20)]
        public float Price { get; set; }

        public int? PizzaCategoryId { get; set; }
        public PizzaCategory? Category { get; set; }

        public PizzaModel()
        {

        }

        public PizzaModel(string name, string description, string imgSource, float price)
        {
            Name = name;
            Description = description;
            ImgSource = imgSource;
            Price = price;
        }
    }
}
