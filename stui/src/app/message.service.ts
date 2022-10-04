import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { Message } from './message';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  hub!: HubConnection;
  $newMessage = new Subject<Message>();
  constructor() { }

  start() {
    if (this.hub)
      return;

    this.hub = new HubConnectionBuilder()
      .withUrl("https://localhost:7136/messagehub")
      .build();

    this.hub.on("newMessage", (message: Message) => {
      this.$newMessage.next(message);
    });

    this.hub.start();
  }

  // invoke(event: string, args: string): Promise<void> {
  //   return this.hub.invoke(event, args);
  // }

  stop() {
    if (!this.hub)
      return;

    this.hub.off("newMessage");
    this.hub.stop();
  }

}