using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditBenchmarkMicroservice.Providers;
using AuthorizationMicroservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuditBenchmarkMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditBenchmarkController : ControllerBase
    {
        private readonly IBenchmarkProvider _benchmarkProvider;
        private readonly ILogger<AuditBenchmarkController> _logger; 

        public AuditBenchmarkController(IBenchmarkProvider benchmarkProvider, ILogger<AuditBenchmarkController> logger)
        {
            _benchmarkProvider = benchmarkProvider;
            _logger = logger;
        }

        [HttpGet]
        [Route("{auditType}")]
        public IActionResult AuditBenchmark(string auditType)
        {
            _logger.LogInformation(" Http GET request " + nameof(AuditBenchmarkController));
            if (string.IsNullOrEmpty(auditType))
            {
                _logger.LogError("Audit Type is empty");
                return BadRequest("No Input");
            }

            if ((auditType != "Internal") && (auditType != "SOX"))
            {
                _logger.LogError("Audit Type is Wrong");
                return BadRequest("Wrong Input");
            }
            try
            {
                var listOfProvider = _benchmarkProvider.GetBenchmark(auditType);
                return Ok(listOfProvider);
            }
            catch (Exception e)
            {
                _logger.LogError(" Exception here" + e.Message + " " + nameof(AuditBenchmarkController));
                return BadRequest(new {message = e.Message});
            }
        }
    }
}
