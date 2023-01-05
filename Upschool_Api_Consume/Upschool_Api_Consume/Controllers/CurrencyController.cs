using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Upschool_Api_Consume.Models;

namespace Upschool_Api_Consume.Controllers
{
    public class CurrencyController : Controller
    {
        public async Task<IActionResult> Index()
        {
			
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri("https://booking-com.p.rapidapi.com/v1/metadata/exchange-rates?currency=TRY&locale=en-gb"),
				Headers =
	{
		{ "X-RapidAPI-Key", "5cc393ee48mshf3e184142efe1acp14b229jsn397efc0cd121" },
		{ "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
	},
			};
			using (var response = await client.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
				var currencies = JsonConvert.DeserializeObject<CurrencyListViewModel>(body);
				
				return View(currencies.exchange_rates); //veri getirme de içiçe olduğu için dgeri burdan alıcaz sadece bunu 
			}
			
        }
    }
}
