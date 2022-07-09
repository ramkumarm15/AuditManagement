using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationMicroservice.Models;

namespace AuditBenchmarkMicroservice.Providers
{
    public interface IBenchmarkProvider
    {
        public BenchmarkData  GetBenchmark(string auditType);
    }
}
