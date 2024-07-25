using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Odata.Service;

namespace Odata.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class MedicalRecordController : ODataController
    {
        private readonly IMedicalRecordSerivce _service;

        public MedicalRecordController(IMedicalRecordSerivce service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var List = await _service.GetAll();

            if (List == null || List.Count == 0)
            {
                return NotFound();
            }
            return Ok(List);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var medical = await _service.GetOne(id);
            if (medical == null)
            {
                return NotFound();
            }
            return Ok(medical);
        }
    }
}
