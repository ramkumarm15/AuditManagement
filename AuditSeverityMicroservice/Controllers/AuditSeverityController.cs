using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditSeverityMicroservice.Models;
using AuditSeverityMicroservice.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuditSeverityMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditSeverityController : ControllerBase
    {
        private readonly ISeverityProvider _severityProvider;


        public AuditSeverityController(ISeverityProvider severityProvider)
        {
            _severityProvider = severityProvider;
        }

        [HttpPost]
        public IActionResult ProjectExecutionStatus([FromBody] AuditRequest? auditRequest)
        {
            if (auditRequest == null) return BadRequest("Request Body is empty");
            if (auditRequest.AuditDetail.Type != "Internal" && auditRequest.AuditDetail.Type != "SOX")
            {
                return BadRequest("Wrong Audit Type");
            }

            try
            {
                _severityProvider.SaveAuditRequest(auditRequest);
                var response = _severityProvider.SeverityResponse(auditRequest);
                return Ok(response);
            }
            catch (Exception e)
            {
                // _log4net.Error(e.Message);
                Console.Write(e);
                return BadRequest();
            }
        }
    }
}
