using dihweb.Data;
using dihweb.Enums;
using dihweb.Helpers;
using dihweb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dihweb.Controllers
{
    public class ManualOperationController : Controller
    {
        private ApplicationDbContext _db;

        public ManualOperationController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var devices = _db.Devices.OrderBy(x => x.OrderIndex).Include(x=>x.DevicesLog).ToList();

            return View(devices);
        }

        public IActionResult DeviceLog(int ID)
        {
            ViewBag.ID = ID;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> startDevice(string DeviceID, string optionValue)
        {
            var device = _db.Devices.Include(x=>x.DeviceType).Where(x=>x.ID == (int.Parse(DeviceID))).FirstOrDefault();

            DevicesLog newDeviceLog = new DevicesLog();
            newDeviceLog.DeviceStatusId = (int)DeviceStatusEnum.Started;
            newDeviceLog.DevicesId = device.ID;
            newDeviceLog.Time = DateTime.Now.ToUniversalTime();
            _db.DevicesLog.Add(newDeviceLog);
            _db.SaveChanges();

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
                        @"            ""start"" : {" + "\n" +
                        @"                ""type"": ""command""," + "\n" +
                        @"                ""value"": """ + optionValue + "\"" + "\n" +
                        @"            }" + "\n" +
                        @"        }" + "\n" +
                        @"    ]" + "\n" +
                        @"}";

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            var content = response.Content;

            return Json(new { Message = "Response from Orion Context Broker: " + content, url = Url.Action("Index", "ManualOperation") });
        }

        [HttpPost]
        public async Task<IActionResult> stopDevice(string DeviceID, string optionValue)
        {
            var device = _db.Devices.Include(x => x.DeviceType).Where(x => x.ID == (int.Parse(DeviceID))).FirstOrDefault();

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
                        @"            ""stop"" : {" + "\n" +
                        @"                ""type"": ""command""," + "\n" +
                        @"                ""value"": ""-1""" + "\n" +
                        @"            }" + "\n" +
                        @"        }" + "\n" +
                        @"    ]" + "\n" +
                        @"}";

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            var content = response.Content;

            return Json(new { Message = "Response from Orion Context Broker: " + content, url = Url.Action("Index", "ManualOperation") });
        }

        [HttpPost]
        public async Task<IActionResult> simulateDeviceAttributes(string DeviceID, string DeviceTypeID)
        {
            string[] FiveAxisStatuses = { "Program Started", "Program Finished", "Homing in Progress", "Tool Changing in Progress"};
            string[] FiveAxisSpeed = { "100", "200", "300", "400" };

            string[] ThreeAxisStatuses = { "Cutting Station Starts", "Laser Cutting Process Running", "Cutting & Stacking Complete" };

            string[] RosPosition = { "1,1", "2,3", "2,2", "4,4", "5,6" };
            string[] RosStatuses = { "Parked", "In Transit" };
            string[] RosCurrentStation = { "A", "B", "C", "D", "None" };
            string[] RosCurrentRoute = { "1", "2", "3", "4", "None" };
            string[] RosObstacleDetected = { "None", "Waiting" };
            string[] RosBatteryLevel = { "10", "20", "30", "40", "50", "60", "70", "80", "90", "100" };

            Random random = new Random();

            var device = _db.Devices.Include(x => x.DeviceType).Where(x => x.ID == (int.Parse(DeviceID))).FirstOrDefault();

            var client = new RestClient(HostingURL.dockerURL + ":1026/v2/op/update");

            var request = new RestRequest("", Method.Post);
            request.AddHeader("fiware-service", "openiot");
            request.AddHeader("fiware-servicepath", "/");
            request.AddHeader("Content-Type", "application/json");

            var body = "";

            if (int.Parse(DeviceTypeID) == (int)DeviceTypeEnum.ThreeAxis)
            {
                body = @"{ " + "\n" +
                             @"  ""actionType"":""update"", " + "\n" +
                             @"  ""entities"":[ " + "\n" +
                             @"    { " + "\n" +
                             @"      ""id"":""" + device.DeviceName + "\"," + "\"type\":\"" + device.DeviceType.IOTAgentType + "\"" + "," + "\n" +
                             @"      ""Status"":{""type"":""Text"", ""value"": """ + ThreeAxisStatuses[random.Next(ThreeAxisStatuses.Length)] + "\"" + "} " + "\n" +
                             @"    } " + "\n" +
                             @"  ] " + "\n" +
                             @"}";
            }
            else if (int.Parse(DeviceTypeID) == (int)DeviceTypeEnum.FiveAxis)
            {
                body = @"{ " + "\n" +
                              @"  ""actionType"":""update"", " + "\n" +
                              @"  ""entities"":[ " + "\n" +
                              @"    { " + "\n" +
                              @"      ""id"":""" + device.DeviceName + "\"," + "\"type\":\"" + device.DeviceType.IOTAgentType + "\"" + "," + "\n" +
                              @"      ""Speed"":{""type"":""Text"", ""value"": """ + FiveAxisSpeed[random.Next(FiveAxisSpeed.Length)]  + "\"" + "}, " + "\n" +
                              @"      ""Status"":{""type"":""Text"", ""value"": """ + FiveAxisStatuses[random.Next(FiveAxisStatuses.Length)] + "\"" + "} " + "\n" +
                              @"    } " + "\n" +
                              @"  ] " + "\n" +
                              @"}";
            }
            else if (int.Parse(DeviceTypeID) == (int)DeviceTypeEnum.ROS2_Transport)
            {
                body = @"{ " + "\n" +
                              @"  ""actionType"":""update"", " + "\n" +
                              @"  ""entities"":[ " + "\n" +
                              @"    { " + "\n" +
                              @"      ""id"":""" + device.DeviceName + "\"," + "\"type\":\"" + device.DeviceType.IOTAgentType + "\"" + "," + "\n" +
                              @"      ""BatteryLevel"":{""type"":""Text"", ""value"": """ + RosBatteryLevel[random.Next(RosBatteryLevel.Length)] + "\"" + "}, " + "\n" +
                              @"      ""Position"":{""type"":""Text"", ""value"": """ + RosPosition[random.Next(RosPosition.Length)] + "\"" + "}, " + "\n" +
                              @"      ""CurrentRoute"":{""type"":""Text"", ""value"": """ + RosCurrentRoute[random.Next(RosCurrentRoute.Length)] + "\"" + "}, " + "\n" +
                              @"      ""CurrentStation"":{""type"":""Text"", ""value"": """ + RosCurrentStation[random.Next(RosCurrentStation.Length)] + "\"" + "}, " + "\n" +
                              @"      ""ObstacleDetected"":{""type"":""Text"", ""value"": """ + RosObstacleDetected[random.Next(RosObstacleDetected.Length)] + "\"" + "}, " + "\n" +
                              @"      ""Status"":{""type"":""Text"", ""value"": """ + RosStatuses[random.Next(RosStatuses.Length)] + "\"" + "} " + "\n" +
                              @"    } " + "\n" +
                              @"  ] " + "\n" +
                              @"}";
            }

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            var content = response.Content;

            return Json(new { Message = "Response from Orion Context Broker: " + content, url = Url.Action("Index", "ManualOperation") });
        }


        [HttpPost]
        public async Task<IActionResult> getDeviceAttributes(string DeviceID, string DeviceTypeID)
        {
            string attrs = "";
            if (int.Parse(DeviceTypeID) == (int)DeviceTypeEnum.ThreeAxis)
            {
                attrs = "Status";
            }
            else if (int.Parse(DeviceTypeID) == (int)DeviceTypeEnum.FiveAxis)
            {
                attrs = "Status,Speed";
            }
            else if (int.Parse(DeviceTypeID) == (int)DeviceTypeEnum.ROS2_Transport)
            {
                attrs = "BatteryLevel,CurrentRoute,CurrentStation,ObstacleDetected,Position,Status";
            }

            var device = _db.Devices.Include(x => x.DeviceType).Where(x => x.ID == (int.Parse(DeviceID))).FirstOrDefault();

            var client = new RestClient(HostingURL.dockerURL + ":1026/v2/entities/" + device.DeviceName + "/?options=keyValues&attrs=" + attrs);

            var request = new RestRequest("", Method.Get);
            request.AddHeader("fiware-service", "openiot");
            request.AddHeader("fiware-servicepath", "/");

            var response = await client.ExecuteAsync(request);
            var content = response.Content;

            return Json(new { Content = content });
        }
        
    }
}
