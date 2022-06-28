using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using dihweb.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dihweb.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SubscriptionsDataController : Controller
    {
        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var client = new RestClient(HostingURL.dockerURL + ":1026/v2/subscriptions");

            var request = new RestRequest("", Method.Get);
            request.AddHeader("fiware-service", "openiot");
            request.AddHeader("fiware-servicepath", "/");

            var response = client.ExecuteAsync(request);
            var content = response.Result.Content;

            var model = JsonConvert.DeserializeObject<List<Models.SubscriptionsModel>>(content);

            var responseModel = model.Select(i => new
            {
                i.id,
                i.description,
                i.status
            }).AsQueryable();

            return Json(DataSourceLoader.Load(responseModel, loadOptions));
        }
    }
}
