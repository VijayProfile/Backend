using Microsoft.EntityFrameworkCore;
using SqlServer.Model;

namespace SqlServer.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Details> OTP { get; set; }
    }
}
