using Microsoft.EntityFrameworkCore;

namespace AuditBenchmarkMicroservice.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Benchmark> Benchmarks { get; set; }
}
