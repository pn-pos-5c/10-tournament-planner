import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import Match from '../../models/Match';
import Player from '../../models/Player';

@Injectable({
  providedIn: 'root'
})
export class DataProviderService { // set winner get matches get players
  private rootUrl = 'http://localhost:5000';

  constructor(private http: HttpClient) {
  }

  getOpenMatches(): Observable<Match[]> {
    return this.http.get<Match[]>(`${this.rootUrl}/api/matches/openMatches`);
  }

  getPlayers(): Observable<Player[]> {
    return this.http.get<Player[]>(`${this.rootUrl}/api/players`);
  }

  setWinner(matchId: number, playerId: number): Observable<Match> {
    return this.http.put<Match>(`${this.rootUrl}/api/matches/setWinner?matchId=${matchId}&playerId=${playerId}`, '');
  }

  deleteAllMatches(): Observable<boolean> {
    return this.http.delete<boolean>(`${this.rootUrl}/api/matches`);
  }
}
