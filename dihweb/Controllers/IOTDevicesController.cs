using dihweb.Data;
using dihweb.Enums;
using dihweb.Helpers;
using dihweb.Models;
using dihweb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dihweb.Controllers
{
    public class IOTDevicesController : Controller
    {
        private ApplicationDbContext _db;

        public IOTDevicesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(IOTDeviceVM model)
        {
            if (model.DeviceTypeId == (int)DeviceTypeEnum.ThreeAxis)
            {
                var client = new RestClient(HostingURL.dockerURL + ":4041/iot/devices");

                var request = new RestRequest("", Method.Post);
                request.AddHeader("fiware-service", "openiot");
                request.AddHeader("fiware-servicepath", "/");
                request.AddHeader("Content-Type", "application/json");

                DeviceRoot deviceModel = new DeviceRoot();
                Device device = new Device();
                device.device_id = model.DeviceID;
                device.entity_name = model.EntityName;
                device.entity_type = model.EntityType;
                device.protocol = "PDI-IoTA-UltraLight";
                device.transport = "HTTP";
                device.endpoint = model.Endpoint;

                Command command1 = new Command();
                command1.name = "start";
                command1.type = "command";
                command1.value = "0";
                device.commands.Add(command1);

                Command command2 = new Command();
                command2.name = "stop";
                command2.type = "command";
                command2.value = "0";
                device.commands.Add(command2);

                Command command3 = new Command();
                command3.name = "auto";
                command3.type = "command";
                command3.value = "0";
                device.commands.Add(command3);

                Attribute attribute1 = new Attribute();
                attribute1.name = "Status";
                attribute1.type = "Text";
                device.attributes.Add(attribute1);

                deviceModel.devices.Add(device);

                string jsonString = JsonConvert.SerializeObject(deviceModel);

                request.AddParameter("text/plain", jsonString, ParameterType.RequestBody);
                var response = await client.ExecuteAsync(request);
                var content = response.Content;

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    // Add device to table
                    var allDevices = _db.Devices.ToList().Max(x => x.OrderIndex);
                    Devices newDevice = new Devices();
                    newDevice.DeviceName = device.entity_name;
                    newDevice.DeviceTypeID = model.DeviceTypeId;

                    if (allDevices != null)
                    {
                        newDevice.OrderIndex = allDevices + 1;
                    }
                    else
                    {
                        newDevice.OrderIndex = 0;
                    }

                    _db.Devices.Add(newDevice);
                    var deviceID = _db.SaveChanges();


                    // Add device log
                    DevicesLog newDeviceLog = new DevicesLog();
                    newDeviceLog.DeviceStatusId = (int)DeviceStatusEnum.Created;
                    newDeviceLog.DevicesId = newDevice.ID;
                    newDeviceLog.Time = DateTime.Now.ToUniversalTime();

                    _db.DevicesLog.Add(newDeviceLog);
                    _db.SaveChanges();

                    string notifyURL = HostingURL.dockerURL + ":3000/api/api/ContextProvider";
                    //string notifyURL = "http://context-provider:3000/api/api/ContextProvider";

                    // Create Subscription
                    await createDeviceSubscription(model.EntityName, "Notify Axis " + model.EntityName, notifyURL, "Axis");

                    return Json(new { Status = 1, Message = "Device created!", url = Url.Action("Index", "IOTDevices") });
                }
                else
                {
                    return Json(new { Status = 0, Message = content });
                }
            }
            else if (model.DeviceTypeId == (int)DeviceTypeEnum.FiveAxis)
            {
                var client = new RestClient(HostingURL.dockerURL + ":4041/iot/devices");

                var request = new RestRequest("", Method.Post);
                request.AddHeader("fiware-service", "openiot");
                request.AddHeader("fiware-servicepath", "/");
                request.AddHeader("Content-Type", "application/json");

                DeviceRoot deviceModel = new DeviceRoot();
                Device device = new Device();
                device.device_id = model.DeviceID;
                device.entity_name = model.EntityName;
                device.entity_type = model.EntityType;
                device.protocol = "PDI-IoTA-UltraLight";
                device.transport = "HTTP";
                device.endpoint = model.Endpoint;

                Command command1 = new Command();
                command1.name = "start";
                command1.type = "command";
                command1.value = "0";
                device.commands.Add(command1);

                Command command2 = new Command();
                command2.name = "stop";
                command2.type = "command";
                command2.value = "0";
                device.commands.Add(command2);

                Command command3 = new Command();
                command3.name = "auto";
                command3.type = "command";
                command3.value = "0";
                device.commands.Add(command3);

                Attribute attribute1 = new Attribute();
                attribute1.name = "Status";
                attribute1.type = "Text";
                device.attributes.Add(attribute1);

                Attribute attribute2 = new Attribute();
                attribute2.name = "Speed";
                attribute2.type = "Text";
                device.attributes.Add(attribute2);

                deviceModel.devices.Add(device);

                string jsonString = JsonConvert.SerializeObject(deviceModel);

                request.AddParameter("text/plain", jsonString, ParameterType.RequestBody);
                var response = await client.ExecuteAsync(request);
                var content = response.Content;

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    // Add device to table
                    var allDevices = _db.Devices.ToList().Max(x => x.OrderIndex);
                    Devices newDevice = new Devices();
                    newDevice.DeviceName = device.entity_name;
                    newDevice.DeviceTypeID = model.DeviceTypeId;

                    if (allDevices != null)
                    {
                        newDevice.OrderIndex = allDevices + 1;
                    }
                    else
                    {
                        newDevice.OrderIndex = 0;
                    }

                    _db.Devices.Add(newDevice);
                    var deviceID = _db.SaveChanges();


                    // Add device log
                    DevicesLog newDeviceLog = new DevicesLog();
                    newDeviceLog.DeviceStatusId = (int)DeviceStatusEnum.Created;
                    newDeviceLog.DevicesId = newDevice.ID;
                    newDeviceLog.Time = DateTime.Now.ToUniversalTime();

                    _db.DevicesLog.Add(newDeviceLog);
                    _db.SaveChanges();

                    string notifyURL = HostingURL.dockerURL + ":3000/api/api/ContextProvider";
                    //string notifyURL = "http://context-provider:3000/api/api/ContextProvider";

                    // Create Subscription
                    await createDeviceSubscription(model.EntityName, "Notify Axis " + model.EntityName, notifyURL, "Axis");

                    return Json(new { Status = 1, Message = "Device created!", url = Url.Action("Index", "IOTDevices") });
                }
                else
                {
                    return Json(new { Status = 0, Message = content });
                }
            }
            else if (model.DeviceTypeId == (int)DeviceTypeEnum.ROS2_Transport)
            {
                var client = new RestClient(HostingURL.dockerURL + ":4041/iot/devices");

                var request = new RestRequest("", Method.Post);
                request.AddHeader("fiware-service", "openiot");
                request.AddHeader("fiware-servicepath", "/");
                request.AddHeader("Content-Type", "application/json");

                DeviceRoot deviceModel = new DeviceRoot();
                Device device = new Device();
                device.device_id = model.DeviceID;
                device.entity_name = model.EntityName;
                device.entity_type = model.EntityType;
                device.protocol = "PDI-IoTA-UltraLight";
                device.transport = "HTTP";
                device.endpoint = model.Endpoint;

                Command command1 = new Command();
                command1.name = "start";
                command1.type = "command";
                command1.value = "0";
                device.commands.Add(command1);

                Command command2 = new Command();
                command2.name = "stop";
                command2.type = "command";
                command2.value = "0";
                device.commands.Add(command2);

                Command command3 = new Command();
                command3.name = "auto";
                command3.type = "command";
                command3.value = "0";
                device.commands.Add(command3);

                Attribute attribute1 = new Attribute();
                attribute1.name = "Position";
                attribute1.type = "Text";
                device.attributes.Add(attribute1);

                Attribute attribute2 = new Attribute();
                attribute2.name = "Status";
                attribute2.type = "Text";
                device.attributes.Add(attribute2);

                Attribute attribute3 = new Attribute();
                attribute3.name = "CurrentStation";
                attribute3.type = "Text";
                device.attributes.Add(attribute3);

                Attribute attribute4 = new Attribute();
                attribute4.name = "CurrentRoute";
                attribute4.type = "Text";
                device.attributes.Add(attribute4);

                Attribute attribute5 = new Attribute();
                attribute5.name = "ObstacleDetected";
                attribute5.type = "Text";
                device.attributes.Add(attribute5);

                Attribute attribute6 = new Attribute();
                attribute6.name = "BatteryLevel";
                attribute6.type = "Text";
                device.attributes.Add(attribute6);

                deviceModel.devices.Add(device);

                string jsonString = JsonConvert.SerializeObject(deviceModel);

                request.AddParameter("text/plain", jsonString, ParameterType.RequestBody);
                var response = await client.ExecuteAsync(request);
                var content = response.Content;

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    // Add device to table
                    var allDevices = _db.Devices.ToList().Max(x => x.OrderIndex);
                    Devices newDevice = new Devices();
                    newDevice.DeviceName = device.entity_name;
                    newDevice.DeviceTypeID = model.DeviceTypeId;

                    if (allDevices != null)
                    {
                        newDevice.OrderIndex = allDevices + 1;
                    }
                    else
                    {
                        newDevice.OrderIndex = 0;
                    }

                    _db.Devices.Add(newDevice);
                    var deviceID = _db.SaveChanges();


                    // Add device log
                    DevicesLog newDeviceLog = new DevicesLog();
                    newDeviceLog.DeviceStatusId = (int)DeviceStatusEnum.Created;
                    newDeviceLog.DevicesId = newDevice.ID;
                    newDeviceLog.Time = DateTime.Now.ToUniversalTime();

                    _db.DevicesLog.Add(newDeviceLog);
                    _db.SaveChanges();

                    string notifyURL = HostingURL.dockerURL + ":3000/api/api/ContextProvider";

                    // Create Subscription
                    await createDeviceSubscription(model.EntityName, "Notify ROS Transport " + model.EntityName, notifyURL, "Ros");

                    return Json(new { Status = 1, Message = "Device created!", url = Url.Action("Index", "IOTDevices") });
                }
                else
                {
                    return Json(new { Status = 0, Message = content });
                }
            }
            else
            {
                return Json(new { Status = 0, Message = "" });
            }
        }

        public async Task createDeviceSubscription(string DeviceName, string Description, string url, string type)
        {
            var client = new RestClient(HostingURL.dockerURL + ":1026/v2/subscriptions/");

            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("fiware-service", "openiot");
            request.AddHeader("fiware-servicepath", "/");
            var body = @"{
" + "\n" +
            @"            ""description"": """ + Description + "\"" + ",\n" +
            @"  ""subject"": {
" + "\n" +
            @"    ""entities"": [
" + "\n" +
            @"      {
" + "\n" +
            @"            ""id"": """ + DeviceName + "\"" + ",\n" +
            @"        ""type"": """ + type + "\"" + "\n" +
            @"      }
" + "\n" +
            @"    ]
" + "\n" +
            @"  },
" + "\n" +
            @"  ""notification"": {
" + "\n" +
            @"    ""onlyChangedAttrs"": true,
" + "\n" +
            @"    ""http"": {
" + "\n" +
            @"            ""url"": """ + url + "\"" + "\n" +
            @"    }
" + "\n" +
            @"  }
" + "\n" +
            @"}";

            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            var content = response.Content;
        }

        public class Command
        {
            public string name { get; set; }
            public string type { get; set; }
            public string value { get; set; }
        }

        public class Attribute
        {
            public string name { get; set; }
            public string type { get; set; }
        }

        public class Device
        {
            public Device()
            {
                commands = new List<Command>();
                attributes = new List<Attribute>();
            }

            public string device_id { get; set; }
            public string entity_name { get; set; }
            public string entity_type { get; set; }
            public string protocol { get; set; }
            public string transport { get; set; }
            public string endpoint { get; set; }
            public List<Command> commands { get; set; }
            public List<Attribute> attributes { get; set; }
        }

        public class DeviceRoot
        {
            public DeviceRoot()
            {
                devices = new List<Device>();
            }
            public List<Device> devices { get; set; }
        }
       

        public async Task<ActionResult> Delete(string deviceID, string service)
        {
            var client = new RestClient(HostingURL.dockerURL + ":4041/iot/devices/" + deviceID);

            var request = new RestRequest("", Method.Delete);
            request.AddHeader("fiware-service", service);
            request.AddHeader("fiware-servicepath", "/");
            var response = await client.ExecuteAsync(request);

            return RedirectToAction("Index");
        }
    }
}
