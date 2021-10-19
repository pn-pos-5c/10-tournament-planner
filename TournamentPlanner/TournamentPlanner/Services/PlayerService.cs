using TournamentDb;
using TournamentPlanner.DTOs;

namespace TournamentPlanner.Services
{
    public class PlayerService
    {
        private readonly TournamentDbContext db;

        public PlayerService(TournamentDbContext db)
        {
            this.db = db;
        }

        public Player AddPlayer(PlayerDto player)
        {
            var addedPlayer = db.Players.Add(new Player
            {
                Firstname = player.Firstname,
                Lastname = player.Lastname,
                Gender = (TournamentDb.Gender)player.Gender
            });
            db.SaveChanges();

            return addedPlayer.Entity;
        }
    }
}
