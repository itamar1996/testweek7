using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using testweek7.Models;

namespace testweek7.Controllers
{
    public class ProductController : Controller
    {
        public string url = "https://fakestoreapi.com";

        [HttpGet]
        public async Task<ActionResult> products()
        {
            List<Product> products = new List<Product>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("/products");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    products = JsonConvert.DeserializeObject<List<Product>>(EmpResponse);
                }
            }
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var jsonContent = JsonConvert.SerializeObject(product);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("/products", contentString);
                string responseBody = await Res.Content.ReadAsStringAsync();

                return Content(responseBody);
                return RedirectToAction(nameof(products));
            }
        }

    }
}
