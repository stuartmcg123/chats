import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { firstValueFrom, Subject, take } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Message } from './message';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  hub!: HubConnection;
  $newMessage = new Subject<Message>();
  
  constructor(private oidc: OidcSecurityService) { }

  start() {
    if (this.hub)
      return;

    this.hub = new HubConnectionBuilder()
      .withUrl(`${environment.apiUrl}/messagehub`, {
        accessTokenFactory: () => firstValueFrom(this.oidc.getAccessToken())
      })
      .build();

    this.hub.on("newMessage", (message: Message) => {
      this.$newMessage.next(message);
    });

    this.hub.start();
  }

  stop() {
    if (!this.hub)
      return;

    this.hub.off("newMessage");
    this.hub.stop();
  }

}