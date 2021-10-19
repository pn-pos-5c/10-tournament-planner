using System;
using System.Collections.Generic;
using System.Linq;
using TournamentDb;

namespace TournamentPlanner.Services
{
    public class MatchService
    {
        private readonly TournamentDbContext db;
        private readonly Random random = new();

        public MatchService()
        {

        }

        public MatchService(TournamentDbContext db)
        {
            this.db = db;
        }
        private int GetCurrentRound()
        {
            if (db.Matches.Where(match => match.Winner == 0).Any()) throw new Exception("Some matches haven't been played yet!");

            return db.Matches.Count() switch
            {
                0 => 1,
                16 => 2,
                24 => 3,
                28 => 4,
                30 => 5,
                _ => throw new Exception("Invalid amount of completet matches")
            };
        }

        public Match AddMatch(int player1Id, int player2Id)
        {
            var player1 = db.Players.Where(player => player.Id == player1Id).FirstOrDefault();
            var player2 = db.Players.Where(player => player.Id == player2Id).FirstOrDefault();

            var addedMatch = db.Matches.Add(new Match
            {
                Player1 = player1,
                Player1Id = player1.Id,
                Player2 = player2,
                Player2Id = player2.Id,
                Round = GetCurrentRound(),
                Winner = 0
            });
            db.SaveChanges();

            return addedMatch.Entity;
        }

        public void CreateMatches()
        {
            var players = new List<Player>();
            if (GetCurrentRound() > 1) players = db.Players.ToList().Where(player => db.Matches.Where(match => match.Round == GetCurrentRound()).ToList().Exists(match => match.Winner == player.Id)).ToList();
            else players = db.Players.ToList();

            for (int i = 0; i < players.Count / 2; i++)
            {
                var player1 = players[random.Next(players.Count)];
                players.Remove(player1);
                var player2 = players[random.Next(players.Count)];
                players.Remove(player2);

                db.Matches.Add(new Match
                {
                    Player1 = player1,
                    Player1Id = player1.Id,
                    Player2 = player2,
                    Player2Id = player2.Id,
                    Round = GetCurrentRound(),
                    Winner = 0
                });
            }
            db.SaveChanges();
        }

        public Match SetWinner(int matchId, int playerId)
        {
            var match = db.Matches.Where(match => match.Id == matchId).FirstOrDefault();
            if (match != null)
            {
                match.Winner = playerId;
                db.SaveChanges();
            }

            return match;
        }

        public List<Match> GetOpenMatches()
        {
            return db.Matches.Where(match => match.Winner == 0).ToList();
        }

        public bool DeleteAllMatches()
        {
            db.Matches.RemoveRange(db.Matches);
            db.SaveChanges();
            return !db.Matches.Any();
        }
    }
}
