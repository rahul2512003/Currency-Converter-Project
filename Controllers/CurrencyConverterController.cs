using CurrencyConverter.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
//using System.Web.Mvc;

namespace CurrencyConverter.Controllers
{
    public class CurrencyConverterController : Controller
    {
        private static readonly string apiUrl = "https://api.exchangerate-api.com/v4/latest/";

        // GET: CurrencyConverter
        public ActionResult Index()
        {
            return View();
        }

        // POST: CurrencyConverter/Convert
        [HttpPost]
        public async Task<ActionResult> Convert(CurrencyConverterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Call the API to get conversion rates
                    var rate = await GetExchangeRate(model.FromCurrency, model.ToCurrency);
                    model.ConvertedAmount = model.Amount * rate;
                }
                catch (Exception)
                {
                    // Handle API errors or invalid input
                    ModelState.AddModelError("", "An error occurred while retrieving the exchange rate.");
                }
            }
            return View("Index", model);
        }

        // Method to fetch the exchange rate from the API
        private async Task<double> GetExchangeRate(string fromCurrency, string toCurrency)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(apiUrl + fromCurrency);
                dynamic data = JsonConvert.DeserializeObject(response);

                // Ensure the API contains the necessary currency pair
                if (data.rates[toCurrency] != null)
                {
                    return data.rates[toCurrency];
                }
                else
                {
                    throw new Exception("Currency pair not available");
                }
            }
        }
    }
}
