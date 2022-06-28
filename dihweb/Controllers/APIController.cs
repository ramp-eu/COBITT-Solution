using dihweb.Data;
using dihweb.Enums;
using dihweb.Helpers;
using dihweb.Hubs;
using dihweb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static dihweb.Controllers.ManualOperationController;

namespace dihweb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private ApplicationDbContext _db;
        IHubContext<ChatHub> Hubcontext;
        private IWebHostEnvironment _hostEnvironment;

        public APIController(ApplicationDbContext db, IHubContext<ChatHub> hubcontext, IWebHostEnvironment environment)
        {
            _db = db;
            Hubcontext = hubcontext;
            _hostEnvironment = environment;
        }


        [HttpGet]
        public IActionResult GetTest()
        {
            return Ok(100);
        }

      

        // Mock Devices (API Simulations)
        //--------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> ThreeAxisEndpoint() 
        {
            string res = "";
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var body = await reader.ReadToEndAsync();

                var data = (JObject)JsonConvert.DeserializeObject(body);

                string path = Path.Combine(_hostEnvironment.WebRootPath, "res.txt");
                using (StreamWriter writer = System.IO.File.AppendText(path))
                {

                    writer.WriteLine(body);
                    writer.Flush();
                }

                if (data.ContainsKey("start"))
                {
                    string value = data["start"].Value<string>();
                    res = "{\"start\" : \"OK\"}";
                }
                else if (data.ContainsKey("stop"))
                {
                    string value = data["stop"].Value<string>();
                    res = "{\"stop\" : \"OK\"}";
                }
                else if (data.ContainsKey("auto"))
                {
                    string value = data["auto"].Value<string>();
                    res = "{\"auto\" : \"OK\"}";
                }
            }
            JObject json = JObject.Parse(res);

            return Ok(json);
        }

        [HttpPost]
        public async Task<IActionResult> FiveAxisEndpoint()
        {
            string res = "";
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var body = await reader.ReadToEndAsync();

                var data = (JObject)JsonConvert.DeserializeObject(body);

                string path = Path.Combine(_hostEnvironment.WebRootPath, "res.txt");
                using (StreamWriter writer = System.IO.File.AppendText(path))
                {

                    writer.WriteLine(body);
                    writer.Flush();
                }

                if (data.ContainsKey("start"))
                {
                    string value = data["start"].Value<string>();
                    res = "{\"start\" : \"OK\"}";
                }
                else if (data.ContainsKey("stop"))
                {
                    string value = data["stop"].Value<string>();
                    res = "{\"stop\" : \"OK\"}";
                }
                else if (data.ContainsKey("auto"))
                {
                    string value = data["auto"].Value<string>();
                    res = "{\"auto\" : \"OK\"}";
                }
            }

            JObject json = JObject.Parse(res);

            return Ok(json);
        }

        [HttpPost]
        public async Task<IActionResult> ROSEndpoint()
        {
            string res = "";
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var body = await reader.ReadToEndAsync();

                var data = (JObject)JsonConvert.DeserializeObject(body);

                if (data.ContainsKey("start"))
                {
                    string value = data["start"].Value<string>();
                    res = "{\"start\" : \"OK\"}";
                }
                else if (data.ContainsKey("stop"))
                {
                    string value = data["stop"].Value<string>();
                    res = "{\"stop\" : \"OK\"}";
                }
                else if (data.ContainsKey("auto"))
                {
                    string value = data["auto"].Value<string>();
                    res = "{\"auto\" : \"OK\"}";
                }
            }
            JObject json = JObject.Parse(res);

            return Ok(json);
        }

        [HttpPost]
        public async Task<IActionResult> ROSTransportEndpoint()
        {
            string res = "";
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var body = await reader.ReadToEndAsync();

                var data = (JObject)JsonConvert.DeserializeObject(body);

                if (data.ContainsKey("start"))
                {
                    string value = data["start"].Value<string>();
                    res = "{\"start\" : \"OK\"}";
                }
                else if (data.ContainsKey("stop"))
                {
                    string value = data["stop"].Value<string>();
                    res = "{\"stop\" : \"OK\"}";
                }
                else if (data.ContainsKey("auto"))
                {
                    string value = data["auto"].Value<string>();
                    res = "{\"auto\" : \"OK\"}";
                }
            }

            JObject json = JObject.Parse(res);

            return Ok(json);
        }
        //--------------------------------------------------------


        // Context Provider
        //--------------------------------------------------------
        
        [HttpPost]
        public async Task<IActionResult> ContextProvider()
        {
            var body = "";
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                body = await reader.ReadToEndAsync();
            }
            
            Root responseData = JsonConvert.DeserializeObject<Root>(body);

            if (responseData.data.Count() > 0)
            {
                string path = Path.Combine(_hostEnvironment.WebRootPath, "res.txt");
                using (StreamWriter writer = System.IO.File.AppendText(path))
                {

                    writer.WriteLine(body);
                    writer.Flush();
                }

                var deviceID = responseData.data.FirstOrDefault().id;

                // Manual Operation
                if (responseData.data.FirstOrDefault().start_status != null)
                {
                    // IF in PENDING mode
                    if (responseData.data.FirstOrDefault().start_status.value == "PENDING")
                    {
                        var device = _db.Devices.Where(x => x.DeviceName == deviceID).FirstOrDefault();

                        // 3 Axis Update Status
                        if (device.DeviceTypeID == (int)DeviceTypeEnum.ThreeAxis)
                        {
                            DevicesLog newDeviceLog = new DevicesLog();
                            newDeviceLog.DeviceStatusId = (int)DeviceStatusEnum.Cutting;
                            newDeviceLog.DevicesId = device.ID;
                            newDeviceLog.Time = responseData.data.FirstOrDefault().start_status.metadata.TimeInstant.value;
                            _db.DevicesLog.Add(newDeviceLog);
                            _db.SaveChanges();
                        }

                        // 5 Axis Update Status
                        if (device.DeviceTypeID == (int)DeviceTypeEnum.FiveAxis)
                        {
                            DevicesLog newDeviceLog = new DevicesLog();
                            newDeviceLog.DeviceStatusId = (int)DeviceStatusEnum.Trimming;
                            newDeviceLog.DevicesId = device.ID;
                            newDeviceLog.Time = responseData.data.FirstOrDefault().start_status.metadata.TimeInstant.value;
                            _db.DevicesLog.Add(newDeviceLog);
                            _db.SaveChanges();
                        }

                        // ROS Update Status
                        if (device.DeviceTypeID == (int)DeviceTypeEnum.ROS2_Transport)
                        {
                            DevicesLog newDeviceLog = new DevicesLog();
                            newDeviceLog.DeviceStatusId = (int)DeviceStatusEnum.Moving;
                            newDeviceLog.DevicesId = device.ID;
                            newDeviceLog.Time = responseData.data.FirstOrDefault().start_status.metadata.TimeInstant.value;
                            _db.DevicesLog.Add(newDeviceLog);
                            _db.SaveChanges();
                        }
                    }

                    if (responseData.data.FirstOrDefault().start_status.value == "OK")
                    {
                        var device = _db.Devices.Where(x => x.DeviceName == deviceID).FirstOrDefault();

                        DevicesLog newDeviceLog = new DevicesLog();
                        newDeviceLog.DeviceStatusId = (int)DeviceStatusEnum.Finished;
                        newDeviceLog.DevicesId = device.ID;
                        newDeviceLog.Time = responseData.data.FirstOrDefault().start_status.metadata.TimeInstant.value;
                        _db.DevicesLog.Add(newDeviceLog);
                        _db.SaveChanges();
                    }
                }
                // Manual Operation
                else if (responseData.data.FirstOrDefault().stop_status != null)
                {
                    if (responseData.data.FirstOrDefault().stop_status.value == "OK")
                    {
                        var device = _db.Devices.Where(x => x.DeviceName == deviceID).FirstOrDefault();

                        DevicesLog newDeviceLog = new DevicesLog();
                        newDeviceLog.DeviceStatusId = (int)DeviceStatusEnum.Stopped;
                        newDeviceLog.DevicesId = device.ID;
                        newDeviceLog.Time = responseData.data.FirstOrDefault().stop_status.metadata.TimeInstant.value;
                        _db.DevicesLog.Add(newDeviceLog);
                        _db.SaveChanges();
                    }
                }
                // Automatic Operation
                else if (responseData.data.FirstOrDefault().auto_status != null)
                {
                    // If in PENDING mode
                    if (responseData.data.FirstOrDefault().auto_status.value == "PENDING")
                    {
                        var device = _db.Devices.Where(x => x.DeviceName == deviceID).FirstOrDefault();
                        var order = _db.Orders.Where(x => x.isActive == true).FirstOrDefault();

                        // 3 Axis Update Status
                        if (device.DeviceTypeID == (int)DeviceTypeEnum.ThreeAxis)
                        {
                            OrderProcess orderProcess = new OrderProcess();
                            orderProcess.DeviceStatusId = (int)DeviceStatusEnum.Cutting;
                            orderProcess.DevicesId = device.ID;
                            orderProcess.Timestamp = responseData.data.FirstOrDefault().auto_status.metadata.TimeInstant.value;
                            orderProcess.OrdersId = order.OrdersId;
                            _db.OrderProcess.Add(orderProcess);
                            _db.SaveChanges();
                        }

                        // 5 Axis Update Status
                        if (device.DeviceTypeID == (int)DeviceTypeEnum.FiveAxis)
                        {
                            OrderProcess orderProcess = new OrderProcess();
                            orderProcess.DeviceStatusId = (int)DeviceStatusEnum.Trimming;
                            orderProcess.DevicesId = device.ID;
                            orderProcess.Timestamp = responseData.data.FirstOrDefault().auto_status.metadata.TimeInstant.value;
                            orderProcess.OrdersId = order.OrdersId;
                            _db.OrderProcess.Add(orderProcess);
                            _db.SaveChanges();
                        }

                        // ROS Update Status
                        if (device.DeviceTypeID == (int)DeviceTypeEnum.ROS2_Transport)
                        {
                            OrderProcess orderProcess = new OrderProcess();
                            orderProcess.DeviceStatusId = (int)DeviceStatusEnum.Moving;
                            orderProcess.DevicesId = device.ID;
                            orderProcess.Timestamp = responseData.data.FirstOrDefault().auto_status.metadata.TimeInstant.value;
                            orderProcess.OrdersId = order.OrdersId;
                            _db.OrderProcess.Add(orderProcess);
                            _db.SaveChanges();
                        }
                    }

                    if (responseData.data.FirstOrDefault().auto_status.value == "OK")
                    {
                        var order = _db.Orders.Where(x => x.isActive == true).FirstOrDefault();
                        var allDevices = _db.Devices.Include(x=>x.DeviceType).OrderBy(x=>x.OrderIndex).ToList();
                        var activeDevice = _db.Devices.Where(x => x.DeviceName == deviceID).FirstOrDefault();

                        // activeDevice is finished
                        OrderProcess orderProcess = new OrderProcess();
                        orderProcess.DeviceStatusId = (int)DeviceStatusEnum.Finished;
                        orderProcess.DevicesId = activeDevice.ID;
                        orderProcess.Timestamp = responseData.data.FirstOrDefault().auto_status.metadata.TimeInstant.value;
                        orderProcess.OrdersId = order.OrdersId;

                        MachineOrderStatus machineOrderStatus = new MachineOrderStatus();
                        machineOrderStatus.Status = true;
                        machineOrderStatus.OrdersId = order.OrdersId;
                        machineOrderStatus.DevicesId = activeDevice.ID;
                        _db.MachineOrderStatus.Add(machineOrderStatus);

                        _db.OrderProcess.Add(orderProcess);
                        _db.SaveChanges();

                        // Get Next Device to execute
                        var nextDevice = allDevices.SkipWhile(x => x != activeDevice).Skip(1).DefaultIfEmpty(null).FirstOrDefault();

                        // If available
                        if (nextDevice != null)
                        {
                            // add to OrderProcess table that device started
                            OrderProcess nextOrderProcess = new OrderProcess();
                            nextOrderProcess.DeviceStatusId = (int)DeviceStatusEnum.Started;
                            nextOrderProcess.DevicesId = nextDevice.ID;
                            nextOrderProcess.Timestamp = responseData.data.FirstOrDefault().auto_status.metadata.TimeInstant.value;
                            nextOrderProcess.OrdersId = order.OrdersId;

                            _db.OrderProcess.Add(nextOrderProcess);
                            _db.SaveChanges();

                            // TODO execute next device api call
                            await autoStartDeviceAsync(nextDevice, "1"); // set number here
                        }
                        else
                        {
                            Thread.Sleep(2000);
                            // Order is finished / set isActive to false
                            order.isActive = false;
                            order.OrderStatusId = (int)OrderStatusEnum.Finished;
                            _db.Entry(order).State = EntityState.Modified;
                            _db.SaveChanges();
                        }
                    }
                }
            }

            // inform SignaR of changes
            await Hubcontext.Clients.All.SendAsync("ReceiveMessage", "API", "Datagrid");

            return Ok(body);
        }

        public async Task autoStartDeviceAsync(Devices device, string optionValue)
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
            await client.ExecuteAsync(request);
        }

        //--------------------------------------------------------


        //--------------------------------------------------------
        //--------------------------------------------------------
        //--------------------------------------------------------
        public class Datum
        {
            public string id { get; set; }
            public string type { get; set; }
            public StopStatus stop_status { get; set; }
            public StartStatus start_status { get; set; }
            public AutoStatus auto_status { get; set; }
            public TimeInstant TimeInstant { get; set; }
        }

        public class Metadata
        {
            public TimeInstant TimeInstant { get; set; }
        }

        public class Root
        {
            public string subscriptionId { get; set; }
            public List<Datum> data { get; set; }
        }

        public class StopStatus
        {
            public string type { get; set; }
            public string value { get; set; }
            public Metadata metadata { get; set; }
        }

        public class StartStatus
        {
            public string type { get; set; }
            public string value { get; set; }
            public Metadata metadata { get; set; }
        }

        public class AutoStatus
        {
            public string type { get; set; }
            public string value { get; set; }
            public Metadata metadata { get; set; }
        }

        public class TimeInstant
        {
            public string type { get; set; }
            public DateTime value { get; set; }
            public Metadata metadata { get; set; }
        }
        //--------------------------------------------------------
        //--------------------------------------------------------
        //--------------------------------------------------------

    }
}
