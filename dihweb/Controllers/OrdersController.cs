using dihweb.Data;
using dihweb.Enums;
using dihweb.Helpers;
using dihweb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dihweb.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext _db;

        public OrdersController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Manage(int OrdersId)
        {
            ViewBag.OrderStatusId = _db.Orders.Find(OrdersId).OrderStatusId;
            ViewBag.OrdersId = OrdersId;
            return View();
        }

        public IActionResult DeleteOrder(int OrdersId)
        {

            var machineOrderStatus = _db.MachineOrderStatus.Where(x => x.OrdersId == OrdersId).ToList();
            foreach (var item in machineOrderStatus)
            {
                _db.MachineOrderStatus.Remove(item);
            }

            var orderProcess = _db.OrderProcess.Where(x => x.OrdersId == OrdersId).ToList();
            foreach (var item in orderProcess)
            {
                _db.OrderProcess.Remove(item);
            }

            var order = _db.Orders.Find(OrdersId);
             _db.Orders.Remove(order);

            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        

        public IActionResult DevicesProcessStatus(int OrdersId)
        {
            ViewBag.OrdersId = OrdersId;
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> StartOrder(int OrderId)
        {
            var allOrders = _db.Orders.Where(x=>x.isActive == true).ToList();

            if (allOrders.Count() == 0)
            {
                var order = _db.Orders.Find(OrderId);

                // Update order as avtive
                order.isActive = true;
                _db.Entry(order).State = EntityState.Modified;
                _db.SaveChanges();

                // TODO  Check height and weight in order to find appropriate board size and pass to start device.
                string optionValue = "1";

                // Find First Device to Start process
                var firstDeviceToStart = _db.Devices.Include(x => x.DeviceType).OrderBy(x => x.OrderIndex).FirstOrDefault();

                // add to OrderProcess table that device started
                OrderProcess orderProcess = new OrderProcess();
                orderProcess.DeviceStatusId = (int)DeviceStatusEnum.Started;
                orderProcess.DevicesId = firstDeviceToStart.ID;
                orderProcess.Timestamp = DateTime.Now.ToUniversalTime();
                orderProcess.OrdersId = OrderId;

                _db.OrderProcess.Add(orderProcess);
                _db.SaveChanges();

                string res = await autoStartDevice(firstDeviceToStart, optionValue);

                return Json(new { Message = "Response from Orion Context Broker: " + res });
            }
            else
            {
                return Json(new { Message = "There is active order in the process. Please wait until is finished!" });
            }
        }

        [HttpPost]
        public async Task<string> autoStartDevice(Devices device, string optionValue)
        {
            var client = new RestClient(HostingURL.dockerURL + ":4041/v2/op/update");
            var request = new RestRequest("", Method.Post);
            request.AddHeader("fiware-service", "openiot");
            request.AddHeader("fiware-servicepath", "/");
            request.AddHeader("Content-Type", "application/json");

            var body = @"{" + "\n" +
                        @"    ""actionType"": ""update""," + "\n" +
                        @"    ""entities"": [" + "\n" +
                        @"        {" + "\n" +
                        @"            ""type"": """ + device.DeviceType.IOTAgentType + "\"" + ",\n" +
                        @"            ""id"": """ + device.DeviceName + "\"" + ",\n" +
                        @"            ""auto"" : {" + "\n" +
                        @"                ""type"": ""command""," + "\n" +
                        @"                ""value"": """ + optionValue + "\"" + "\n" +
                        @"            }" + "\n" +
                        @"        }" + "\n" +
                        @"    ]" + "\n" +
                        @"}";

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            var content = response.Content;

            return "Response from Orion Context Broker: " + content;
        }
    }
}
