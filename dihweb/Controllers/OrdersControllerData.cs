using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using dihweb.Data;
using dihweb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dihweb.Controllers
{
    [Route("api/[controller]/[action]")]
    public class OrdersControllerData : Controller
    {
        private ApplicationDbContext _context;

        public OrdersControllerData(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            var orders = _context.Orders.Select(i => new
            {
                i.OrdersId,
                i.Name,
                i.Weight,
                i.Height,
                i.OrderStatusId,
                i.OrderDate,
            }).OrderBy(x=>x.OrdersId);

            return Json(await DataSourceLoader.LoadAsync(orders, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderStatus(DataSourceLoadOptions loadOptions)
        {
            var orders = _context.OrderStatus.Select(i => new
            {
                i.OrderStatusId,
                i.Status
            });

            return Json(await DataSourceLoader.LoadAsync(orders, loadOptions));
        }


        [HttpGet]
        public async Task<IActionResult> GetMachineOrderStatus(int OrderId, DataSourceLoadOptions loadOptions)
        {
            var orders = _context.MachineOrderStatus.Where(x=>x.OrdersId == OrderId).Select(i => new
            {
                i.Orders,
                i.MachineOrderStatusId,
                i.Status,
                i.Devices
            }).OrderBy(x => x.MachineOrderStatusId);

            return Json(await DataSourceLoader.LoadAsync(orders, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var model = new Orders();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Orders.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.OrdersId });
        }




        

        [HttpPut]
        public async Task<IActionResult> UpdateMachineStatus(int key, string values)
        {
            var model = await _context.MachineOrderStatus.FirstOrDefaultAsync(item => item.MachineOrderStatusId == key);
            if (model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateMachineStatusModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values)
        {
            var model = await _context.Orders.FirstOrDefaultAsync(item => item.OrdersId == key);
            if (model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        private void PopulateModel(Orders model, IDictionary values)
        {
            string OrdersId = nameof(Orders.OrdersId);
            string Name = nameof(Orders.Name);
            string Weight = nameof(Orders.Weight);
            string Height = nameof(Orders.Height);
            string OrderStatus = nameof(Orders.OrderStatusId);
            string OrderDate = nameof(Orders.OrderDate);

            if (values.Contains(OrdersId))
            {
                model.OrdersId = Convert.ToInt32(values[OrdersId]);
            }

            if (values.Contains(Name))
            {
                model.Name = Convert.ToString(values[Name]);
            }

            if (values.Contains(Weight))
            {
                model.Weight = Convert.ToInt32(values[Weight]);
            }

            if (values.Contains(Height))
            {
                model.Height = Convert.ToInt32(values[Height]);
            }

            if (values.Contains(OrderStatus))
            {
                model.OrderStatusId = Convert.ToInt32(values[OrderStatus]);
            }

            if (values.Contains(OrderDate))
            {
                model.OrderDate = Convert.ToDateTime(values[OrderDate]);
            }
        }

        private void PopulateMachineStatusModel(MachineOrderStatus model, IDictionary values)
        {
            string MachineOrderStatusId = nameof(MachineOrderStatus.MachineOrderStatusId);
            string Status = nameof(MachineOrderStatus.Status);
            string OrdersId = nameof(MachineOrderStatus.OrdersId);
            string DevicesId = nameof(MachineOrderStatus.DevicesId);

            if (values.Contains(MachineOrderStatusId))
            {
                model.MachineOrderStatusId = Convert.ToInt32(values[MachineOrderStatusId]);
            }

            if (values.Contains(Status))
            {
                model.Status = Convert.ToBoolean(values[Status]);
            }

            if (values.Contains(OrdersId))
            {
                model.OrdersId = Convert.ToInt32(values[OrdersId]);
            }

            if (values.Contains(DevicesId))
            {
                model.DevicesId = Convert.ToInt32(values[DevicesId]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState)
        {
            var messages = new List<string>();

            foreach (var entry in modelState)
            {
                foreach (var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderProcess(int OrderId, DataSourceLoadOptions loadOptions)
        {
            var orderProcess = _context.OrderProcess.Where(x=>x.OrdersId == OrderId).Select(i => new
            {
                i.OrderProcessId,
                i.Timestamp,
                i.Devices,
                i.DeviceStatus,
            });

            return Json(await DataSourceLoader.LoadAsync(orderProcess, loadOptions));
        }
    }
}
