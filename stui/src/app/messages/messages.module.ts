import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MessagesRoutingModule } from './messages-routing.module';
import { MessageViewerComponent } from './message-viewer/message-viewer.component';
import { MessageComponent } from './message/message.component';
import { NewMessageComponent } from './new-message/new-message.component';
import { MessageContainerComponent } from './message-container/message-container.component';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    MessageViewerComponent,
    MessageComponent,
    NewMessageComponent,
    MessageContainerComponent],
  imports: [
    CommonModule,
    MessagesRoutingModule,
    ReactiveFormsModule
  ]
})
export class MessagesModule { }
