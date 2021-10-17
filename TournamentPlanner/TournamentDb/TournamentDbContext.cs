using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TournamentDb
{
    public class TournamentDbContext : DbContext
    {
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }

        public TournamentDbContext(DbContextOptions<TournamentDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

    public class TournamentDbContextFactory : IDesignTimeDbContextFactory<TournamentDbContext>
    {
        public TournamentDbContext CreateDbContext(string[]? args = null)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var optionsBuilder = new DbContextOptionsBuilder<TournamentDbContext>();
            optionsBuilder.UseSqlite(configuration["ConnectionStrings:DefaultConnection"]);

            return new TournamentDbContext(optionsBuilder.Options);
        }
    }
}
