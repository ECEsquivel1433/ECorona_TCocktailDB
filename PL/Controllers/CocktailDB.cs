using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ML;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace PL.Controllers
{
    public class CocktailDB : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {

            ML.Cocktail cocktail = new ML.Cocktail();
            cocktail.Drinks = new List<object>();


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/");

                var responseTask = client.GetAsync("v1/1/search.php?s=");
                responseTask.Wait(); //esperar a que se resuelva la llamada al servicio

                var result = responseTask.Result;

                ML.Result result1 = new ML.Result();
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<ML.Cocktail>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Drinks)
                    {
                        ML.Cocktail resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cocktail>(resultItem.ToString());
                        cocktail.Drinks.Add(resultItemList);
                    }
                    return View(cocktail);
                }
                return View(cocktail);
            }
        }

        [HttpPost]
        public ActionResult GetAll(ML.Cocktail cocktail)
        {
            
            if (cocktail.Nombre == null)
            {
                cocktail.Drinks = new List<object>();


                using (var client = new HttpClient())
                {

                    ML.Result result2 = new ML.Result();
                    client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/");

                    var responseTask = client.GetAsync("v1/1/search.php?s=" + cocktail.Ingrediente);
                    responseTask.Wait(); //esperar a que se resuelva la llamada al servicio

                    var result = responseTask.Result;

                    ML.Result result1 = new ML.Result();
                    if (result.IsSuccessStatusCode)
                    {

                        var readTask = result.Content.ReadAsAsync<ML.Cocktail>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Drinks)
                        {
                            ML.Cocktail resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cocktail>(resultItem.ToString());
                            cocktail.Drinks.Add(resultItemList);
                        }
                        return View(cocktail);
                    }
                    return View(cocktail);
                }
            }
            else
            {
                cocktail.Drinks = new List<object>();


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/");

                    var responseTask = client.GetAsync("v1/1/search.php?s=" + cocktail.Nombre);
                    responseTask.Wait(); //esperar a que se resuelva la llamada al servicio

                    var result = responseTask.Result;

                    ML.Result result1 = new ML.Result();
                    if (result.IsSuccessStatusCode)
                    {

                        var readTask = result.Content.ReadAsAsync<ML.Cocktail>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Drinks)
                        {
                            ML.Cocktail resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cocktail>(resultItem.ToString());
                            cocktail.Drinks.Add(resultItemList);
                        }
                        return View(cocktail);
                    }
                    return View(cocktail);
                }
            }

        }

        [HttpGet]
        public ActionResult GetAllDynamic()
        {
            ML.Cocktail cocktail = new ML.Cocktail();
            cocktail.Drinks = new List<object>();


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/");

                var responseTask = client.GetAsync("v1/1/search.php?s=");
                responseTask.Wait(); //esperar a que se resuelva la llamada al servicio

                var result = responseTask.Result;

                ML.Result result1 = new ML.Result();
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<ML.Cocktail>();
                    readTask.Wait();

                    foreach (dynamic resultItem in readTask.Result.Drinks)
                    {
                        //ML.Cocktail resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cocktail>(resultItem.ToString());
                        cocktail.strDrink = resultItem.strdrink;
                        cocktail.strIngredient1 = "https://www.thecocktaildb.com/images/ingredients/" + resultItem.strIngredient1 + "-Small.png";
                        cocktail.strIngredient2 = "https://www.thecocktaildb.com/images/ingredients/" + resultItem.strIngredient2 + "-Small.png";
                        cocktail.strIngredient3 = "https://www.thecocktaildb.com/images/ingredients/" + resultItem.strIngredient3 + "-Small.png";
                        cocktail.strIngredient4 = "https://www.thecocktaildb.com/images/ingredients/" + resultItem.strIngredient4 + "-Small.png";
                        cocktail.strIngredient5 = "https://www.thecocktaildb.com/images/ingredients/" + resultItem.strIngredient5 + "-Small.png";
                        cocktail.strIngredient5 = "https://www.thecocktaildb.com/images/ingredients/" + resultItem.strIngredient6 + "-Small.png";
                        cocktail.strIngredient5 = "https://www.thecocktaildb.com/images/ingredients/" + resultItem.strIngredient7 + "-Small.png";
                        cocktail.Drinks.Add(cocktail);
                    }
                    return View(cocktail);
                }
                return View(cocktail);
            }

        }

    }
}
