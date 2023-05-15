using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
