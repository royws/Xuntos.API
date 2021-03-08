// "Mediator” Between Model/Repository/Db
using Microsoft.EntityFrameworkCore;
using Xuntos.API.Models;

namespace Xuntos.API.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> opt) : base(opt)
        {

        }
        // Represent Case as DbSet to be mapped to db.
        public DbSet<Case> Cases { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Technique> Techniques { get; set; }
        public DbSet<CaseTechnique> CaseTechniques { get; set; }
    }
}
