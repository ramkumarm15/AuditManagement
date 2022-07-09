using AuditSeverityMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace AuditSeverityMicroservice.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<AuditRequestDB> AuditRequest { get; set; }
    public DbSet<AuditResponseDB> AuditResponse { get; set; }
}
