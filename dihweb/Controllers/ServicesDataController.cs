using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using dihweb.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace dihweb.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ServicesDataController : Controller
    {
        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var client = new RestClient(HostingURL.dockerURL + ":4041/iot/services");

            var request = new RestRequest("", Method.Get);
            request.AddHeader("fiware-service", "openiot");
            request.AddHeader("fiware-servicepath", "/");
            var body = @"";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);

            var response = client.ExecuteAsync(request);
            var content = response.Result.Content;

            var model = JsonConvert.DeserializeObject<Models.ServicesModel>(content);
            
            var responseModel = model.services.Select(i => new
            {
                i._id,
                i.apikey,
                i.service,
                i.entity_type
            }).AsQueryable();

            return Json(DataSourceLoader.Load(responseModel, loadOptions));
        }
    }
}
