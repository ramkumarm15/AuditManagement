using System.ComponentModel.DataAnnotations;

namespace AuditChecklistMicroservice.Models;

public class QuestionData
{
    [Required] public int QuestionNo { get; set; }
    [Required] public string Question { get; set; }
}
