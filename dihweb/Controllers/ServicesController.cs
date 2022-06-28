using dihweb.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dihweb.Controllers
{
    public class ServicesController : Controller
    {
        private static Random random = new Random();


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync()
        {
            var client = new RestClient(HostingURL.dockerURL + ":4041/iot/services");

            var request = new RestRequest("", Method.Post);
            request.AddHeader("fiware-service", "openiot");
            request.AddHeader("fiware-servicepath", "/");
            request.AddHeader("Content-Type", "application/json");

            string apiKey = RandomString(26);

            ServicesVM service = new ServicesVM();
            service.apikey = apiKey;
            service.cbroker = "http://orion:1026";
            service.entity_type = "Thing";
            service.resource = "/iot/json";

            IOTServices all_services = new IOTServices();
            all_services.services.Add(service);

            string jsonString = JsonConvert.SerializeObject(all_services);

            request.AddParameter("text/plain", jsonString, ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            var content = response.Content;

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return Json(new { Status = 1, Message = "Service created!", url = Url.Action("Index", "Services") });
            }
            else
            {
                return Json(new { Status = 0, Message = content });
            }
        }


        public async Task<ActionResult> Delete(string apiKey, string service)
        {
            var client = new RestClient(HostingURL.dockerURL + ":4041/iot/services/?resource=/iot/json&apikey=" + apiKey);

            var request = new RestRequest("", Method.Delete);
            request.AddHeader("fiware-service", service);
            request.AddHeader("fiware-servicepath", "/");
            var response = await client.ExecuteAsync(request);

            return RedirectToAction("Index");
        }

        public class IOTServices {
            public IOTServices()
            {
                services = new List<ServicesVM>();
            }
            public List<ServicesVM> services { get; set; }
        }

        public class ServicesVM
        {
            public string apikey { get; set; }
            public string cbroker { get; set; }
            public string entity_type { get; set; }
            public string resource { get; set; }
        }

        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
