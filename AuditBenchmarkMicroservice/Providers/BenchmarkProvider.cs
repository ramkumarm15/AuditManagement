using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditBenchmarkMicroservice.Models;
using AuthorizationMicroservice.Models;


namespace AuditBenchmarkMicroservice.Providers
{
    public class BenchmarkProvider : IBenchmarkProvider
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BenchmarkProvider> _logger;

        public BenchmarkProvider(ApplicationDbContext context, ILogger<BenchmarkProvider> logger)
        {
            _context = context;
            _logger = logger;
        }

        public BenchmarkData GetBenchmark(string auditType)
        {
            _logger.LogInformation(" Http GET request " + nameof(BenchmarkProvider));

            try
            {
                _logger.LogInformation("Getting Benchmark constraints");
                var constraintList = _context.Benchmarks.Where(m => m.auditType == auditType).Select(x =>
                        new BenchmarkData() {auditType = x.auditType, benchmarkConstraint = x.benchmarkConstraint})
                    .FirstOrDefault();
                return constraintList;
            }
            catch (Exception e)
            {
                _logger.LogError(" Exception here" + e.Message + " " + nameof(BenchmarkProvider));
                return null;
            }
        }
    }
}
