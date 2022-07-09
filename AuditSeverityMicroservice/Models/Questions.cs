using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverityMicroservice.Models
{
    public class Questions
    {
        [Required] public bool Question1 { get; set; }
        [Required] public bool Question2 { get; set; }
        [Required] public bool Question3 { get; set; }
        [Required] public bool Question4 { get; set; }
        [Required] public bool Question5 { get; set; }
    }
}
