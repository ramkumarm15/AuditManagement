using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuditSeverityMicroservice.Models;

public class Benchmark
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] public string auditType { get; set; }
    [Required] public int benchmarkConstraint { get; set; }
}
