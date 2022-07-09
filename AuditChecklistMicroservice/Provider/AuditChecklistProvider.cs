using AuditChecklistMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace AuditChecklistMicroservice.Provider;

public class AuditChecklistProvider : IAuditChecklist
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AuditChecklistProvider> _logger;

    public AuditChecklistProvider(ApplicationDbContext context, ILogger<AuditChecklistProvider> logger)
    {
        _logger = logger;
        _context = context;
    }

    public List<QuestionData> QuestionsProvider(string auditType)
    {
        _logger.LogInformation("Getting questions from database based on audit type");
        return _context.Questions.Where(x => x.AuditType == auditType).Select(m => new QuestionData()
                {QuestionNo = m.QuestionNo, Question = m.Question})
            .ToList();
    }
}
