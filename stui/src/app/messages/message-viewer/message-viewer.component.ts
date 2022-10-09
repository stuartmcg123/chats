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
  page: number = 0;
  pageSize: number = 5;
  noMoreMessages = false;

  constructor(
    private httpMessageService: MessageHttpService,
    private messageService: MessageService) { }

  ngOnInit(): void {
    this.httpMessageService
      .get(this.page, this.pageSize)
      .subscribe((c: Message[]) => this.messages = c);

    this.messageService.$newMessage
      .pipe(takeUntil(this.beingDestroyed))
      .subscribe(c =>
        this.messages.push(c)
      );
  }

  loadMore() {
    this.page += 1;
    this.httpMessageService
      .get(this.page, this.pageSize)
      .subscribe((c: Message[]) => {
        if (Array.isArray(c) && !c.length || c.length < this.pageSize)
          this.noMoreMessages = true;
        c = c.concat(this.messages);
        this.messages = c;
      });

  }

  messageDeleted(id: string) {
    alert(`Message with the id ${id} was deleted.`)
  }

  ngOnDestroy(): void {
    this.beingDestroyed.next();
    this.beingDestroyed.unsubscribe();
  }
}
