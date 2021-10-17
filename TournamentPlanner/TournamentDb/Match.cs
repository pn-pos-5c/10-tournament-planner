using System.ComponentModel.DataAnnotations;

namespace TournamentDb
{
    public class Match
    {
        public int Id { get; set; }
        [Range(1, 5)]
        public int Round { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public int Winner { get; set; }
    }
}
