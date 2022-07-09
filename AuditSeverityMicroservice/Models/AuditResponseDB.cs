using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverityMicroservice.Models
{
    public class AuditResponseDB
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuditId { get; set; }

        [Required] public string ProjectName { get; set; }
        [Required] public string ProjectExecutionStatus { get; set; }
        [Required] public string RemedialActionDuration { get; set; }
    }
}
