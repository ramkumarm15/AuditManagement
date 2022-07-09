using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverityMicroservice.Models
{
    public class AuditRequestDB
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required] public string ProjectName { get; set; }
        [Required] public string ProjectManagerName { get; set; }
        [Required] public string ApplicationOwnerName { get; set; }
        [Required] public string Type { get; set; }
        [Required] public string Date { get; set; }
        [Required] public bool Question1 { get; set; }
        [Required] public bool Question2 { get; set; }
        [Required] public bool Question3 { get; set; }
        [Required] public bool Question4 { get; set; }
        [Required] public bool Question5 { get; set; }
    }
}
