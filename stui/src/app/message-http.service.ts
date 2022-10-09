import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { Message } from './message';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MessageHttpService {
  readonly messageUrl = `${environment.apiUrl}/api/messages`
  constructor(private http: HttpClient) { }

  get(page: number = 0, pageSize: number = 10): Observable<Message[]> {
    return this.http
      .get<Message[]>(`${this.messageUrl}?page=${page}&pageSize=${pageSize}`)
      .pipe(take(1));
  }

  delete(id: string): Observable<void> {
    return this.http
      .delete<void>(`${this.messageUrl}/${id}`)
      .pipe(take(1));
  }
}
