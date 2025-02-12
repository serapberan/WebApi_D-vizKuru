﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Upschool_Api_Consume.Models;

namespace Upschool_Api_Consume.Controllers
{
    public class MovieController : Controller
    {
        public async Task <IActionResult> Index()
        {
			List<MoiveListViewModel> moives = new List<MoiveListViewModel>();
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,    //istek atıyoruz
				RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
				Headers =
	{
		{ "X-RapidAPI-Key", "5cc393ee48mshf3e184142efe1acp14b229jsn397efc0cd121" },
		{ "X-RapidAPI-Host", "imdb-top-100-movies.p.rapidapi.com" },
	},
			};
			using (var response = await client.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
				moives = JsonConvert.DeserializeObject<List<MoiveListViewModel>>(body);
				return View(moives);
			}
		
        }
    }
}
