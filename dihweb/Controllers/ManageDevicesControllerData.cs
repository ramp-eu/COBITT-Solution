using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using dihweb.Data;
using dihweb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dihweb.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ManageDevicesControllerData : Controller
    {
        private ApplicationDbContext _context;

        public ManageDevicesControllerData(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            var devices = _context.Devices.Select(i => new
            {
                i.ID,
                i.DeviceName,
                i.DeviceType,
                i.OrderIndex
            });
            devices = devices.OrderBy(x => x.OrderIndex);
            return Json(await DataSourceLoader.LoadAsync(devices, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetLog(int ID, DataSourceLoadOptions loadOptions)
        {
            var log = (from devices in _context.Devices
                       join deviceLog in _context.DevicesLog
                       on devices.ID equals deviceLog.DevicesId
                       where devices.ID == ID
                       select new DeviceLogVM
                       {
                           DeviceName = devices.DeviceName,
                           Status = deviceLog.DeviceStatus.Status,
                           Time = deviceLog.Time
                       }).OrderBy( x=>x.Time);
                   
            return Json(await DataSourceLoader.LoadAsync(log, loadOptions));
        }

        [HttpPut]
        public IActionResult UpdateTask(int key, string values)
        {
            var task = _context.Devices.First(o => o.ID == key);
            var oldOrderIndex = task.OrderIndex;

            JsonConvert.PopulateObject(values, task);


            var sortedTasks = _context.Devices.OrderBy(t => t.OrderIndex).ToList();
            sortedTasks.Remove(task);

            if (oldOrderIndex != task.OrderIndex)
            {
                if (oldOrderIndex < task.OrderIndex)
                {
                    for (var i = oldOrderIndex; i < task.OrderIndex; i++)
                    {
                        if (sortedTasks.ElementAt(i.Value).OrderIndex > 0)
                        {
                            sortedTasks.ElementAt(i.Value).OrderIndex--;
                        }
                    }
                }
                else if (oldOrderIndex > task.OrderIndex)
                {
                    for (var i = task.OrderIndex; i < oldOrderIndex; i++)
                    {
                        if (sortedTasks.ElementAt(i.Value).OrderIndex < sortedTasks.Count())
                        {
                            sortedTasks.ElementAt(i.Value).OrderIndex++;
                        }
                    }
                }
            }

            _context.Entry(task).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(task);
        }
    }
}
