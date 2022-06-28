using dihweb.Helpers;
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
    public class SubscriptionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(SubscriptionsVM model)
        {
            var client = new RestClient(HostingURL.dockerURL + ":1026/v2/subscriptions");

            var request = new RestRequest("", Method.Post);
            request.AddHeader("fiware-service", "openiot");
            request.AddHeader("fiware-servicepath", "/");
            request.AddHeader("Content-Type", "application/json");


            Root not = new Root();
            not.description = model.Description;
            not.throttling = 5;

            Entity ent = new Entity();
            ent.idPattern = ".*";

            Subject sub = new Subject();
            sub.entities.Add(ent);
            not.subject = sub;

            Http http = new Http();
            http.url = "http://cygnus:5050/notify";

            Notification notification = new Notification();
            notification.attrsFormat = "legacy";
            notification.http = http;
            not.notification = notification;

            string jsonString = JsonConvert.SerializeObject(not);

            request.AddParameter("text/plain", jsonString, ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            var content = response.Content;

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return Json(new { Status = 1, Message = "Subscription created!", url = Url.Action("Index", "Subscriptions") });
            }
            else
            {
                return Json(new { Status = 0, Message = content });
            }
        }

        public class Entity
        {
            public string idPattern { get; set; }
        }

        public class Http
        {
            public string url { get; set; }
        }

        public class Notification
        {
            public Http http { get; set; }
            public string attrsFormat { get; set; }
        }

        public class Root
        {
            public string description { get; set; }
            public Subject subject { get; set; }
            public Notification notification { get; set; }
            public int throttling { get; set; }
        }

        public class Subject
        {
            public Subject()
            {
                entities = new List<Entity>();
            }
            public List<Entity> entities { get; set; }
        }

        public async Task<ActionResult> Delete(string subscriptionID)
        {
            var client = new RestClient(HostingURL.dockerURL + ":1026/v2/subscriptions/" + subscriptionID);

            var request = new RestRequest("", Method.Delete);
            request.AddHeader("fiware-service", "openiot");
            request.AddHeader("fiware-servicepath", "/");
            var response = await client.ExecuteAsync(request);

            return RedirectToAction("Index");
        }
    }
}
