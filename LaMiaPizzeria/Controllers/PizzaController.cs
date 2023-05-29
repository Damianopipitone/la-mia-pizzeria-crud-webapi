using LaMiaPizzeria.DataBase;
using LaMiaPizzeria.Models;
using LaMiaPizzeria.Models.ModelForView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Data;

namespace LaMiaPizzeria.Controllers
{
    [Authorize]
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {   
            using (PizzaContext db = new PizzaContext())
            {
                List<PizzaModel> pizze = db.Pizze.ToList();
                
                return View(pizze);
            }
            
        }

        public IActionResult Details(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                PizzaModel? pizzaDetails = db.Pizze.Where(pizza => pizza.id == id).FirstOrDefault();

                if (pizzaDetails != null)
                {
                    return View("Details", pizzaDetails);
                }
                else
                {
                    return NotFound($"La pizza con id {id} non è stata trovata!");
                }
            }

        }

        

        [HttpGet]
        public IActionResult Create()
        {   

            using (PizzaContext context = new PizzaContext())
            {
                List<PizzaCategory> pizzaCategories = context.pizzaCategories.ToList();

                PizzaListCategory modelForView = new PizzaListCategory();
                modelForView.Pizzas = new PizzaModel();
                modelForView.PizzasCategories = pizzaCategories; 
                
                return View("Create", modelForView);
            }

            
           
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create(PizzaModel data)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", data);
            }

            using (PizzaContext context = new PizzaContext())
            {
                List<PizzaCategory> pizzaCategories = context.pizzaCategories.ToList();

                context.Pizze.Add(data);

                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Update(int id)
        {
            using (PizzaContext context = new PizzaContext())
            {
                PizzaModel? pizzeToEdit = context.Pizze.Where(pizze => pizze.id == id).FirstOrDefault();

                if (pizzeToEdit == null)
                {
                    return NotFound("La pizza che stai cercando non esiste");
                } else
                {
                    return View("Update", pizzeToEdit);
                }
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id,  PizzaModel updatedPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", updatedPizza);
            } else
            {   

                using(PizzaContext db = new PizzaContext())
                {
                    PizzaModel? pizzaToUpdate = db.Pizze.Where(pizza => pizza.id == id).FirstOrDefault();
                    if (pizzaToUpdate == null)
                    {
                        return NotFound();
                    } else
                    {
                        pizzaToUpdate.Name = updatedPizza.Name;
                        pizzaToUpdate.Description = updatedPizza.Description;
                        pizzaToUpdate.ImgSource = updatedPizza.ImgSource;
                        pizzaToUpdate.Price = updatedPizza.Price;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
        }

        public IActionResult Delete(int id)
        {
            using(PizzaContext db = new PizzaContext())
            {
                PizzaModel? pizzaToDelete = db.Pizze.Where(pizza => pizza.id == id).FirstOrDefault();
                if (pizzaToDelete == null)
                {
                    return NotFound();
                } else
                {
                    db.Remove(pizzaToDelete);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
        }
    }

}
