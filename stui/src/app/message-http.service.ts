import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { Message } from './message';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MessageHttpService {

  constructor(private http: HttpClient) { }

  get(): Observable<Message[]> {
    return this.http
      .get<Message[]>('https://localhost:7136/api/messages')
      .pipe(take(1));
  }

  delete(id: string): Observable<void> {
    alert('About to delete!')
    return this.http
      .delete<void>(`${environment.apiUrl}/api/messages/${id}`)
      .pipe(take(1));
  }
}
