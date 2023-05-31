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
        [HttpGet]
        public IActionResult GetPizzas()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<PizzaModel> pizzas = db.Pizze.ToList();
                return Ok(pizzas);
            }
        }

        [HttpGet]
        public IActionResult SearchByTitle(string Name)
        {
            using (PizzaContext db = new PizzaContext())
            {
                PizzaModel? pizzaToSearch = db.Pizze.Where(pizza => pizza.Name.Contains(Name)).FirstOrDefault();
                if (pizzaToSearch == null)
                {
                    return NotFound("La pizza richiesta non esiste!");
                }
                else
                {
                    return Ok(pizzaToSearch);
                }
            }
        }

        [HttpGet("{id}")]
        public IActionResult SearchById(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                PizzaModel? pizzaId = db.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();
                if (pizzaId == null)
                {
                    return NotFound("La pizza richiesta non esiste!");
                }
                else
                {
                    return Ok(pizzaId);
                }
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] PizzaModel pizza)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                using (PizzaContext db = new PizzaContext())
                {
                    db.Add(pizza);
                    db.SaveChanges();

                    return Ok();
                }
            }
        } 
    }
}
