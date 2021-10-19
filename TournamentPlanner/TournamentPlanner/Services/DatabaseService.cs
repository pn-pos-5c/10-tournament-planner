using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TournamentDb;

namespace TournamentPlanner.Services
{
    public class DatabaseService : IHostedService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public DatabaseService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(ParseCsv, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void ParseCsv()
        {
            // credits to Michael Wiesinger for next 3 lines
            using IServiceScope scope = scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TournamentDbContext>();
            var matchesService = scope.ServiceProvider.GetRequiredService<MatchService>();

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            string[] lines = File.ReadAllLines("./Assets/players.csv");

            for (int i = 0; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(",");

                if (!Enum.TryParse(line[2], out Gender gender)) continue;

                Player player = new()
                {
                    Firstname = line[0],
                    Lastname = line[1],
                    Gender = gender
                };

                dbContext.Players.Add(player);
                dbContext.SaveChanges();
            }

            var matchService = new MatchService();
            matchesService.CreateMatches();
        }
    }
}
