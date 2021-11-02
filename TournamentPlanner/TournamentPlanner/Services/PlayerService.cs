using System;
using System.Collections.Generic;
using System.Linq;
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
                Gender = Enum.Parse<Gender>(player.Gender)
            });
            db.SaveChanges();

            return addedPlayer.Entity;
        }

        public List<PlayerDto> GetPlayers()
        {
            return db.Players.Select(player => new PlayerDto
            {
                Id = player.Id,
                Firstname = player.Firstname,
                Lastname = player.Lastname,
                Gender = player.Gender.ToString()
            }).ToList();
        }
    }
}
