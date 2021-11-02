export default interface Match {
  id: number;
  round: number;
  player1Id: number;
  player2Id: number;
  // 0=no winner, 1=player1, 2=player2
  winner: number;
}
