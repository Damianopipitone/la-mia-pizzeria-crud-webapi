using LaMiaPizzeria.DataBase;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaMiaPizzeria.Controllers.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzaAPIController : ControllerBase
    {
        public IActionResult GetPizzas()
        {
            using(PizzaContext db = new PizzaContext())
            {
                List<PizzaModel> pizzas = db.Pizze.ToList();
                return Ok(pizzas);
            }
        }
    }
}
