using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace la_mia_pizzeria_static.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllPizzaString(string? words)
        {
            using (PizzaContext context = new PizzaContext())
            {
                // api/pizzas/GetAllPizzaString/
                
                if (words == null)
                {
                    IQueryable<Pizza> pizza = context.Pizza;

                    //Ritorna tutte le pizze
                    return Ok(pizza.ToList());
                }

                // api/pizzas/GetAllPizzaString ? words = [STRING]
                List<Pizza> pizze = context.Pizza.Where(pizze=>pizze.Nome.Contains(words)).ToList();
                
                // se la pizza non esiste da error 404
                if (pizze.Count <= 0)
                {
                    return NotFound();
                }

                //return la pizza inserita nella [STRING]
                return Ok(pizze);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Pizza> GetPizzaId(int id)
        {
            using (PizzaContext ctx = new PizzaContext())
            {
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                var pizza = ctx.Pizza.Where(p => p.Id == id)
                                     .Include(p => p.Category)
                                     .Include(p => p.Ingredients)
                                     .FirstOrDefault();

                if (pizza == null)
                    return NotFound();

                var json = JsonSerializer.Serialize(pizza, options);
                return Content(json, "application/json");
            }
        }



        [HttpPost]
        public IActionResult Create(Pizza data)
        {
            using (PizzaContext ctx = new PizzaContext())
            {
                Pizza pizza = new Pizza();
                pizza.Nome = data.Nome;
                pizza.Descrizione = data.Descrizione;
                pizza.Prezzo = data.Prezzo;
                pizza.Img = data.Img;
                pizza.CategoryId = data.CategoryId;

                if (data.Ingredients != null)
                {
                    foreach (Ingredients ingredient in data.Ingredients)
                    {
                        int ingredientId = ingredient.Id;
                        Ingredients qualcosa = ctx.Ingredients.Where(ing => ing.Id == ingredientId).FirstOrDefault();
                        pizza.Ingredients.Add(qualcosa);
                    }
                }

                ctx.Add(pizza);
                ctx.SaveChanges();

                return Ok();
            }            
        }
    }
}
