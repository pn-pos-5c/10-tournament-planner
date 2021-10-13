using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentDb
{
    public class Match
    {
        public int Id { get; set; }
        public int Round { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player? Winner { get; set; }
    }
}
