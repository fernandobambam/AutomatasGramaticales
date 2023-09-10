using AutomatasGramaticalesApi.Models;
using AutomatasGramaticalesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutomatasGramaticalesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidationController : ControllerBase
    {
        private readonly IValidationService _validationService; 

        public ValidationController(IValidationService validationService)
        {
            _validationService = validationService;
        }

        [HttpPost]
        public ActionResult Validate([FromBody]string[] lines)
        {
            Compiler result = _validationService.validate(lines);

            return Ok(result);
        }
    }
}
