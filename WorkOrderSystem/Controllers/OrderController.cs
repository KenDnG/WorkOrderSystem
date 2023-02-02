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
        public IActionResult Index(int pageIndex)
        {
            var dataPaging = new Pagination<spPagingOrderResult>();
            var page = pageIndex == 0 ? 1 : pageIndex;
           
            var data = orderrepo.GetPaging(page, 10,null).Result;
            ViewBag.CurrentPage = page;
            ViewBag.PagingOrder = data;
            if(data.RecordCount != 0)
            {
                ViewBag.Disable = "true";
            }
            else
            {
                ViewBag.Disable = "false";
            }
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
        public IActionResult Detail(string WorkOrderCode,string State)
        {
            ViewModel data = new ViewModel();
            Order order = new Order();
            try
            {
                if(State == "Edit")
                {
                    order = orderrepo.GetData(WorkOrderCode).Result;
                    data.Order = order;
                }
            }
            catch (Exception)
            {

                throw;
            }
            ViewBag.State = State;
            data.State = State;
            return View("~/Views/Order/Detail.cshtml",data);
        }
        public IActionResult Delete(string WorkOrderCode)
        {
            Order order = new Order();
            order.workorder_code = WorkOrderCode;
            if(orderrepo.Delete(order).Result)
            {
                //deleted
            }
            else
            {
                //not deleted
            }
           return RedirectToAction("Index");
        }
        public IActionResult Save(ViewModel item)
        {
            try
            {
                if(item.State == "Add")
                {
                    if(orderrepo.Insert(item.Order).Result)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //insert error
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    if(orderrepo.Update(item.Order).Result)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //update error
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }
        }

    }
}
