using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using dihweb.Data;
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
    public class IOTDevicesDataController : Controller
    {
        private ApplicationDbContext _context;

        public IOTDevicesDataController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var client = new RestClient(HostingURL.dockerURL + ":4041/iot/devices/");

            var request = new RestRequest("", Method.Get);
            request.AddHeader("fiware-service", "openiot");
            request.AddHeader("fiware-servicepath", "/");
            var body = @"";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);

            var response = client.ExecuteAsync(request);
            var content = response.Result.Content;

            var model = JsonConvert.DeserializeObject<Models.IOTDevicesModel>(content);

            var responseModel = model.devices.Select(i => new
            {
                i.device_id,
                i.service,
                i.entity_type,
                i.entity_name,
                i.transport
            }).AsQueryable();

            return Json(DataSourceLoader.Load(responseModel, loadOptions));
        }


        
        [HttpGet]
        public async Task<IActionResult> DeviceTypeLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.DeviceType
                         orderby i.DeviceTypeID
                         select new
                         {
                             DeviceTypeId = i.DeviceTypeID,
                             Type = i.Type
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }
    }
}
