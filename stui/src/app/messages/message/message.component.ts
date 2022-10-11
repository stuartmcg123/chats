import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
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
  token!: string;
  @Output()
  deleted = new EventEmitter<string>();
  constructor(
    private messageService: MessageHttpService,
    private oidcService: OidcSecurityService) { }

  ngOnInit(): void {
    this.oidcService
      .getUserData()
      .subscribe(userData => {
        this.token = userData.sub;
      });
  }

  delete() {
    if (!confirm('Are you sure?'))
      return;
    this.messageService
      .delete(this.message.id)
      .subscribe(c => {
        this.deleted.next(this.message.id)
      })
  }
}
