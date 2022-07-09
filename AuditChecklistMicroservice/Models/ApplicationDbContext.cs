using Microsoft.EntityFrameworkCore;

namespace AuditChecklistMicroservice.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Questions> Questions { get; set; }
}
