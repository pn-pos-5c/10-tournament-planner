namespace TournamentPlanner.DTOs
{
    public class MatchDto
    {
        public int Id { get; set; }
        public int Round { get; set; }
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public int Winner { get; set; }
    }
}
