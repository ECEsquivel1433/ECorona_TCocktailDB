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

        //[HttpGet]
        //public ActionResult GetAllDynamic()
        //{
        //    ML.Cocktail cocktail = new ML.Cocktail();
        //    cocktail.Drinks = new List<object>();


        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/");

        //        var responseTask = client.GetAsync("v1/1/search.php?s=");
        //        responseTask.Wait(); //esperar a que se resuelva la llamada al servicio

        //        var result = responseTask.Result;

        //        ML.Result result1 = new ML.Result();
        //        if (result.IsSuccessStatusCode)
        //        {

        //            var readTask = result.Content.ReadAsAsync<ML.Cocktail>();
        //            readTask.Wait();

        //            foreach (dynamic resultItem in readTask.Result.Drinks)
        //            {
        //                //ML.Cocktail resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cocktail>(resultItem.ToString());
        //                cocktail.strDrink = resultItem.strdrink;
        //                cocktail.strIngredient1 = "https://www.thecocktaildb.com/images/ingredients/" + resultItem.strIngredient1 + "-Small.png";
        //                cocktail.strIngredient2 = "https://www.thecocktaildb.com/images/ingredients/" + resultItem.strIngredient2 + "-Small.png";
        //                cocktail.strIngredient3 = "https://www.thecocktaildb.com/images/ingredients/" + resultItem.strIngredient3 + "-Small.png";
        //                cocktail.strIngredient4 = "https://www.thecocktaildb.com/images/ingredients/" + resultItem.strIngredient4 + "-Small.png";
        //                cocktail.strIngredient5 = "https://www.thecocktaildb.com/images/ingredients/" + resultItem.strIngredient5 + "-Small.png";
        //                cocktail.strIngredient5 = "https://www.thecocktaildb.com/images/ingredients/" + resultItem.strIngredient6 + "-Small.png";
        //                cocktail.strIngredient5 = "https://www.thecocktaildb.com/images/ingredients/" + resultItem.strIngredient7 + "-Small.png";
        //                cocktail.Drinks.Add(cocktail);
        //            }
        //            return View(cocktail);
        //        }
        //        return View(cocktail);
        //    }

        //}

        [HttpGet]
        public IActionResult GetAllDynamic()
        {
            ML.Cocktail cocktail = new ML.Cocktail();
            cocktail.Drinks = new List<object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/");

                var responseTask = client.GetAsync("v1/1/search.php?s=");
                responseTask.Wait(); //esperar a que se resuelva la llamada al servicio

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<dynamic>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.drinks)
                    {
                        ML.Cocktail resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cocktail>(resultItem.ToString());
                        //cocktail.Drinks.Add(resultItemList);
                        ML.Cocktail cocktail1 = new ML.Cocktail();
                        cocktail1.strDrink = resultItemList.strDrink;
                        cocktail1.strDrinkThumb= resultItemList.strDrinkThumb;
                        cocktail1.Ingredient1 = resultItemList.strIngredient1;
                        cocktail1.Ingredient2 = resultItemList.strIngredient2;
                        cocktail1.Ingredient3 = resultItemList.strIngredient3;
                        cocktail1.Ingredient4 = resultItemList.strIngredient4;
                        cocktail1.Ingredient5 = resultItemList.strIngredient5;
                        cocktail1.Ingredient6 = resultItemList.strIngredient6;
                        cocktail1.Ingredient7 = resultItemList.strIngredient7;
                        cocktail1.Ingredient8 = resultItemList.strIngredient8;
                        cocktail1.Ingredient9 = resultItemList.strIngredient9;
                        cocktail1.Ingredient10 = resultItemList.strIngredient10;
                        cocktail1.strIngredient1 = "https://www.thecocktaildb.com/images/ingredients/" + resultItemList.strIngredient1 + "-Small.png";
                        cocktail1.strIngredient2 = "https://www.thecocktaildb.com/images/ingredients/" + resultItemList.strIngredient2 + "-Small.png";
                        cocktail1.strIngredient3 = "https://www.thecocktaildb.com/images/ingredients/" + resultItemList.strIngredient3 + "-Small.png";
                        cocktail1.strIngredient4 = "https://www.thecocktaildb.com/images/ingredients/" + resultItemList.strIngredient4 + "-Small.png";
                        cocktail1.strIngredient5 = "https://www.thecocktaildb.com/images/ingredients/" + resultItemList.strIngredient5 + "-Small.png";
                        cocktail1.strIngredient6 = "https://www.thecocktaildb.com/images/ingredients/" + resultItemList.strIngredient6 + "-Small.png";
                        cocktail1.strIngredient7 = "https://www.thecocktaildb.com/images/ingredients/" + resultItemList.strIngredient7 + "-Small.png";
                        cocktail1.strIngredient8 = "https://www.thecocktaildb.com/images/ingredients/" + resultItemList.strIngredient7 + "-Small.png";
                        cocktail1.strIngredient9 = "https://www.thecocktaildb.com/images/ingredients/" + resultItemList.strIngredient7 + "-Small.png";
                        cocktail1.strIngredient10 = "https://www.thecocktaildb.com/images/ingredients/" + resultItemList.strIngredient7 + "-Small.png";
                        cocktail.Drinks.Add(cocktail1);

                    }
                }
            }
            return View(cocktail);
        }

    }
}
