namespace LaMiaPizzeria.Models.ModelForView
{
    public class PizzaListCategory
    {
        public PizzaModel Pizzas { get; set; }

        public List<PizzaCategory>? PizzasCategories { get; set; }
    }
}
