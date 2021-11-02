import {Component, OnInit} from '@angular/core';
import {DataProviderService} from '../../services/data-provider.service';
import Player from '../../../models/Player';
import Match from '../../../models/Match';

@Component({
  selector: 'app-tournament',
  templateUrl: './tournament.component.html',
  styleUrls: ['./tournament.component.scss']
})
export class TournamentComponent implements OnInit {
  players: Player[] = [];
  matches: Match[] = [];

  constructor(private dataProvider: DataProviderService) {
  }

  ngOnInit(): void {
    this.dataProvider.getPlayers().subscribe(resolve => this.players = resolve);
    this.dataProvider.getOpenMatches().subscribe(resolve => this.matches = resolve);
  }

  startTournament(): void {
    this.dataProvider.deleteAllMatches().subscribe(deleted => {
      if (deleted) {
        this.dataProvider.getOpenMatches().subscribe(matches => this.matches = matches);
      }
    });
  }

  getPlayer(playerId: number): string {
    const player = this.players.find(p => p.id === playerId);
    if (player) {
      return `${player.firstname} ${player.lastname}: ${player.gender}`;
    }
    return 'unknown';
  }

  setWinner(matchId: number, playerId: number): void {
    // check if match already has a winner
    const match = this.matches.find(m => m.id === matchId);
    if (match) {
      if (match.winner !== 0) {
        return;
      }
    } else {
      return;
    }

    this.dataProvider.setWinner(matchId, playerId).subscribe(resolve => {
      this.dataProvider.getOpenMatches().subscribe(matches => this.matches = matches);
    });
  }
}
