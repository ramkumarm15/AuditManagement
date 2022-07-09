using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuditChecklistMicroservice.Models;

public class Questions
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] public int QuestionNo { get; set; }
    [Required] public string Question { get; set; }
    [Required] public string AuditType { get; set; }
}
