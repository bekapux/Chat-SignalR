import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ChatRoutingModule } from './chat-routing.module';
import { ChatComponent } from './chat.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [ChatComponent, NavComponent],
  imports: [CommonModule, ChatRoutingModule, FormsModule],
})
export class ChatModule {}
