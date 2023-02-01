using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net.Security;
using WorkOrderSystem.Models;
using WorkOrderSystem.Repository;

namespace WorkOrderSystem.Controllers
{
    public class OrderController : Controller
    {
        private OrderRepo orderrepo;
        private DBLocalKenContext dbContext;
        public OrderController(DBLocalKenContext _db)
        {
            try
            {
                this.dbContext = _db;
                this.orderrepo = new OrderRepo(_db);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public IActionResult DownloadData()
        {
            try
            {
                //get data from api
                string url = @"https://id.cfm-system.com/api/coding-test/59427201-fa7c-46e9-84f6-83f98ff9d219";
                var options = new RestClientOptions(url);
                options.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                var client = new RestClient(options);
                var request = new RestRequest(url, Method.Get);
                var response = client.Execute(request);
                var dataResponse = JsonConvert.DeserializeObject<List<Order>>(response.Content);
                //send data to db
                if(orderrepo.Insert(dataResponse).Result)
                {
                    return RedirectToAction("Index");
                }

            }
            catch (Exception)
            {

                throw;
            }
            return View("~/Views/Order/Index.cshtml");
        }
    }
}
