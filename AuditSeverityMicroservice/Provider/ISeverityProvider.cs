using AuditSeverityMicroservice.Models;

namespace AuditSeverityMicroservice.Provider;

public interface ISeverityProvider
{
    public AuditResponseDB SeverityResponse(AuditRequest auditRequest);

    public Benchmark? GetBenchmark(string auditType);

    public void SaveAuditRequest(AuditRequest auditRequest);
}
