﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Upschool_Api_Consume.Models;

namespace Upschool_Api_Consume.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task< IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44355/api/Category");
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<CategoryViewModel>>(jsondata);
                return View(result);
            }
            else
            {
                ViewBag.responseMessage = "Bir hata oluştu!";
                  return View();
            }
        }


        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryViewModel c)
        {
            c.Status = true;
            var client = _httpClientFactory.CreateClient();
            var JsonData = JsonConvert.SerializeObject(c);  //jsona veri gönderiyoruz
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:44355/api/Category", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.responseMessage = "Bir hata ile karşılaşıldı";
                return View();
            }
          

        }

        [HttpGet]
        public async Task <IActionResult> UpdateCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44355/api/Category/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CategoryViewModel>(jsonData);
                return View(result);
            }
            else
            {
                ViewBag.responseMessage = "Bir hata ile karşılaşıldı";
                return View();
            }

        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryViewModel c)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(c);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44355/api/Category", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.responseMessage = "Bir hata ile karşılaşıldı";
                return View();
            }
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:44355/api/Category/{id}");
            return RedirectToAction("Index");
        }


    }
}
