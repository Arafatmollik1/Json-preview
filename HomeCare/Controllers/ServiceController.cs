using HomeCare.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace HomeCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        [HttpGet("services")]
        public ActionResult<IEnumerable<Service>> GetServices()
        {
            return Ok(ReadServicesFromJsonFile());
        }

        [HttpGet("services/{serviceId:Guid}")]
        public ActionResult<Service> GetService([FromRoute] Guid serviceId)
        {
            var allSercvices = ReadServicesFromJsonFile();
            var service = allSercvices.FirstOrDefault(s => s.ServiceId == serviceId);
            return service is null ? Ok("Service not found") : Ok(service);
        }

        [HttpGet("services/{status}")]
        public ActionResult<IEnumerable<Service>> GetServicesByStatus([FromRoute] string status)
        {
            if (status == ServiceStatuses.ACTIVE || status == ServiceStatuses.NEW || status == ServiceStatuses.MODIFIED)
            {
                var allSercvices = ReadServicesFromJsonFile();
                var statusService = allSercvices.Where(s => s.ServiceStatus == status).ToList();
                return Ok(statusService);
            }
            return BadRequest("Invalid Status");
        }

        [HttpPost("services")]
        public ActionResult<string> AddService([FromBody] Service service)
        {
            if (service.ServiceStatus == ServiceStatuses.ACTIVE || service.ServiceStatus == ServiceStatuses.NEW || service.ServiceStatus == ServiceStatuses.MODIFIED)
            {
                var allSercvices = ReadServicesFromJsonFile();

                if (allSercvices.Any(s => s.ServiceId == service.ServiceId))
                {
                    return BadRequest("Service Id already exists");
                }

                allSercvices.Add(service);
                FlushAndWriteServicesToJsonFile(allSercvices);
                return Ok("Service Successfully Added");
            }
            return BadRequest("Invalid Status");
        }

        #region Private Helper Methods

        private List<Service> ReadServicesFromJsonFile()
        {
            var path = GetJsonFilePath();
            var text = System.IO.File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<Service>>(text);
        }

        private void FlushAndWriteServicesToJsonFile(List<Service> services)
        {
            var path = GetJsonFilePath();
            System.IO.File.WriteAllText(path, "");
            var json = JsonConvert.SerializeObject(services);
            System.IO.File.WriteAllText(path, json);
        }

        private string GetJsonFilePath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), $"JSON\\services.json");
        }

        #endregion
    }
}
