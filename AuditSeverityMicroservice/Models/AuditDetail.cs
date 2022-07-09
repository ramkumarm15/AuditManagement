using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverityMicroservice.Models
{
    public class AuditDetail
    {
        [Required] public string Type { get; set; }
        [Required] public string Date { get; set; }
        [Required] public Questions questions { get; set; }
    }
}
