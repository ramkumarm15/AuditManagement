using AuditChecklistMicroservice.Models;

namespace AuditChecklistMicroservice.Provider;

public interface IAuditChecklist
{
    public List<QuestionData> QuestionsProvider(string auditType);
}
