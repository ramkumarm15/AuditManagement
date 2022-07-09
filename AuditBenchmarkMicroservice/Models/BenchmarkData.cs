using System.ComponentModel.DataAnnotations;

namespace AuthorizationMicroservice.Models;

public class BenchmarkData
{
    [Required] public string auditType { get; set; }
    [Required] public int benchmarkConstraint { get; set; }
}
