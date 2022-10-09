import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { Message } from '../../message';
import { MessageHttpService } from '../../message-http.service';
import { MessageService } from '../../message.service';

@Component({
  selector: 'app-message-viewer',
  templateUrl: './message-viewer.component.html',
  styleUrls: ['./message-viewer.component.scss']
})
export class MessageViewerComponent implements OnInit, OnDestroy {
  messages!: Message[];
  beingDestroyed = new Subject<void>();

  constructor(
    private httpMessageService: MessageHttpService,
    private messageService: MessageService) { }

  ngOnInit(): void {
    this.httpMessageService
      .get()
      .subscribe((c: Message[]) => this.messages = c);

    this.messageService.$newMessage
      .pipe(takeUntil(this.beingDestroyed))
      .subscribe(c =>
        this.messages.push(c)
      );
  }

  messageDeleted(id: string) {
    alert(`Message with the id ${id} was deleted.`)
  }

  ngOnDestroy(): void {
    this.beingDestroyed.next();
    this.beingDestroyed.unsubscribe();
  }
}
