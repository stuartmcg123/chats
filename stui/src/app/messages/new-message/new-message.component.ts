import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, UntypedFormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { Message } from '../../message';
import { MessageHttpService } from '../../message-http.service';
import { MessageService } from '../../message.service';

@Component({
  selector: 'app-new-message',
  templateUrl: './new-message.component.html',
  styleUrls: ['./new-message.component.scss']
})
export class NewMessageComponent implements OnInit, OnDestroy {
  private $messages!: Observable<Message[]>;
  form = this.fb.group({
    'message': ['', Validators.required]
  })

  get message(): FormControl {
    return this.form.get('message') as FormControl;
  }

  constructor(
    private messageService: MessageService,
    private fb: UntypedFormBuilder) { }

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
        .invoke("SendMessage", { body: this.message.value, to: 'bobby' })
        .then(c => this.message.setValue(null))
        .catch(e => console.error(e));
    } catch (err) {
      console.error(err)
    }
  }
}
