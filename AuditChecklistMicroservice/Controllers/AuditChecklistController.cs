using AuditChecklistMicroservice.Provider;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuditChecklistMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuditChecklistController : ControllerBase
    {
        private readonly IAuditChecklist _auditChecklist;   
        private readonly ILogger<AuditChecklistController> _logger;

        public AuditChecklistController(IAuditChecklist auditChecklist, ILogger<AuditChecklistController> logger)
        {
            _logger = logger;
            _auditChecklist = auditChecklist;
        }


        [HttpGet]
        [Route("{auditType}")]
        [Authorize]
        public IActionResult AuditCheckListQuestions([FromRoute] string auditType)
        {
            _logger.LogInformation(" Http GET request " + nameof(AuditChecklistController));
            if (string.IsNullOrEmpty(auditType))
            {
                _logger.LogInformation("Audit Type is empty");
                return BadRequest("No Input");
            }

            if ((auditType != "Internal") && (auditType != "SOX"))
            {
                _logger.LogInformation("Audit Type is Wrong");
                return BadRequest("Wrong Input");
            }

            try
            {
                _logger.LogInformation("Getting questions of Audit type");
                var list = _auditChecklist.QuestionsProvider(auditType);
                return Ok(list);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Exception Occured at" + nameof(AuditChecklistController) + " " + e.Message);
                return StatusCode(500);
            }
        }
    }
}
