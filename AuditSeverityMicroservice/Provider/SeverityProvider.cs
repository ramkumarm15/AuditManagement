using AuditSeverityMicroservice.Models;
using Newtonsoft.Json;

namespace AuditSeverityMicroservice.Provider;

public class SeverityProvider : ISeverityProvider
{
    // Uri baseAddress;
    private readonly HttpClient _client;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<SeverityProvider> _logger;

    private readonly List<AuditResponse> _remedialAction = new List<AuditResponse>()
    {
        new()
        {
            RemedialActionDuration = "No Action Needed",
            ProjectExecutionStatus = "GREEN"
        },
        new()
        {
            RemedialActionDuration = "Action to be taken in 2 weeks",
            ProjectExecutionStatus = "RED"
        },
        new()
        {
            RemedialActionDuration = "Action to be taken in 1 week",
            ProjectExecutionStatus = "RED"
        }
    };

    public SeverityProvider(ApplicationDbContext context, ILogger<SeverityProvider> logger)
    {
        _context = context;
        _logger = logger;
        _client = new HttpClient();
    }

    public AuditResponseDB SeverityResponse(AuditRequest auditRequest)
    {
        _logger.LogInformation("Received request for creating audit response");
        var auditResponse = new AuditResponseDB();

        var benchMarkDetails = GetBenchmark(auditRequest.AuditDetail.Type);
        int countOfNoQuestions = 0, acceptableNoQuestions = 0;

        if (auditRequest.AuditDetail.questions.Question1 == false)
            countOfNoQuestions++;
        if (auditRequest.AuditDetail.questions.Question2 == false)
            countOfNoQuestions++;
        if (auditRequest.AuditDetail.questions.Question3 == false)
            countOfNoQuestions++;
        if (auditRequest.AuditDetail.questions.Question4 == false)
            countOfNoQuestions++;
        if (auditRequest.AuditDetail.questions.Question5 == false)
            countOfNoQuestions++;

        if (auditRequest.AuditDetail.Type == benchMarkDetails?.auditType)
            acceptableNoQuestions = benchMarkDetails.benchmarkConstraint;
        else if (auditRequest.AuditDetail.Type == benchMarkDetails?.auditType)
            acceptableNoQuestions = benchMarkDetails.benchmarkConstraint;

        _logger.LogInformation("Creating Audit response...");
        switch (auditRequest.AuditDetail.Type)
        {
            case "Internal" when countOfNoQuestions <= acceptableNoQuestions:
                auditResponse.ProjectName = auditRequest.ProjectName;
                auditResponse.RemedialActionDuration = _remedialAction[0].RemedialActionDuration;
                auditResponse.ProjectExecutionStatus = _remedialAction[0].ProjectExecutionStatus;

                _context.AuditResponse.Add(auditResponse);
                _context.SaveChanges();
                break;
            case "Internal" when countOfNoQuestions > acceptableNoQuestions:
                auditResponse.ProjectName = auditRequest.ProjectName;
                auditResponse.RemedialActionDuration = _remedialAction[1].RemedialActionDuration;
                auditResponse.ProjectExecutionStatus = _remedialAction[1].ProjectExecutionStatus;

                _context.AuditResponse.Add(auditResponse);
                _context.SaveChanges();
                break;
            case "SOX" when countOfNoQuestions <= acceptableNoQuestions:
                auditResponse.ProjectName = auditRequest.ProjectName;
                auditResponse.RemedialActionDuration = _remedialAction[0].RemedialActionDuration;
                auditResponse.ProjectExecutionStatus = _remedialAction[0].ProjectExecutionStatus;

                _context.AuditResponse.Add(auditResponse);
                _context.SaveChanges();
                break;
            case "SOX" when countOfNoQuestions > acceptableNoQuestions:
                auditResponse.ProjectName = auditRequest.ProjectName;
                auditResponse.RemedialActionDuration = _remedialAction[2].RemedialActionDuration;
                auditResponse.ProjectExecutionStatus = _remedialAction[2].ProjectExecutionStatus;

                _context.AuditResponse.Add(auditResponse);
                _context.SaveChanges();
                break;
        }

        return auditResponse;
    }

    public Benchmark? GetBenchmark(string auditType)
    {
        _logger.LogInformation("Get benchmark constraint from benchmark microservice");
        var benchmark = new Benchmark();
        var response =
            _client.GetAsync($"https://localhost:44354/api/AuditBenchmark/{auditType}").Result;
        if (!response.IsSuccessStatusCode) return benchmark;
        var data = response.Content.ReadAsStringAsync().Result;
        benchmark = JsonConvert.DeserializeObject<Benchmark>(data);

        return benchmark;
    }

    public void SaveAuditRequest(AuditRequest auditRequest)
    {
        _logger.LogInformation("Save audit request to db");
        var auditRequestDb = new AuditRequestDB
        {
            ProjectName = auditRequest.ProjectName,
            ProjectManagerName = auditRequest.ProjectManagerName,
            ApplicationOwnerName = auditRequest.ApplicationOwnerName,
            Type = auditRequest.AuditDetail.Type,
            Date = auditRequest.AuditDetail.Date,
            Question1 = auditRequest.AuditDetail.questions.Question1,
            Question2 = auditRequest.AuditDetail.questions.Question2,
            Question3 = auditRequest.AuditDetail.questions.Question3,
            Question4 = auditRequest.AuditDetail.questions.Question4,
            Question5 = auditRequest.AuditDetail.questions.Question5
        };

        _context.AuditRequest.Add(auditRequestDb);
        _context.SaveChanges();
    }
}
