namespace LaMiaPizzeria.Models
{
    public class PizzaCategory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public List<PizzaModel>? Pizze { get; set; }

        public PizzaCategory()
        {

        }

        public PizzaCategory(string Name, string? description)
        {
            this.Name = Name;
            this.Description = description;
            this.Pizze = new List<PizzaModel>();
        }

    }
}
