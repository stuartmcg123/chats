import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';
import { Message } from '../../message';
import { MessageHttpService } from '../../message-http.service';
import { MessageService } from '../../message.service';

@Component({
  selector: 'app-message-viewer',
  templateUrl: './message-viewer.component.html',
  styleUrls: ['./message-viewer.component.scss']
})
export class MessageViewerComponent implements OnInit {
  messages!: Message[];

  constructor(
    private httpMessageService: MessageHttpService,
    private messageService: MessageService,
    private oidc:OidcSecurityService) { }

  ngOnInit(): void {
    this.httpMessageService
      .get()
      .subscribe((c:Message[]) => this.messages = c);


      this.messageService.$newMessage.subscribe(c =>
        this.messages.push(c)
      );
  }

}
