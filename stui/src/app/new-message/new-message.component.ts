import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { Message } from '../message';
import { MessageHttpService } from '../message-http.service';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-new-message',
  templateUrl: './new-message.component.html',
  styleUrls: ['./new-message.component.scss']
})
export class NewMessageComponent implements OnInit, OnDestroy {
  private $messages!: Observable<Message[]>;
  form = this.fb.group({
    'message':['', Validators.required]
  })
  constructor(
    private messageService: MessageService,
    private fb: FormBuilder) { }

  ngOnInit(): void {
    this.messageService
      .start();
  }

  ngOnDestroy(): void {
    this.messageService
      .stop();
  }

  create() {
    try {
      this.messageService
        .hub
        .invoke("SendMessage", {body: this.form.get('message')?.value, to:'bobby'})
        .then(c => console.log('Success'))
        .catch(e => console.error(e));
    } catch (err) {
      console.error(err)
    }
  }
}
