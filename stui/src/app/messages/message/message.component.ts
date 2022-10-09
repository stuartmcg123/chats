import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Message } from 'src/app/message';
import { MessageHttpService } from 'src/app/message-http.service';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.scss']
})
export class MessageComponent implements OnInit {
  @Input()
  message!: Message;

  @Output()
  deleted = new EventEmitter<string>();
  constructor(private messageService: MessageHttpService) { }

  ngOnInit(): void {
  }

  delete() {
    this.messageService
      .delete(this.message.id)
      .subscribe(c => {
        this.deleted.next(this.message.id)
      })
  }
}
