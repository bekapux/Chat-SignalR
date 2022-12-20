import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AuthResponse } from '../models/auth/auth-response.model';
import { MessageModel } from '../models/message.model';
import { AuthService } from '../services/auth.service';
import {
  HttpTransportType,
  HubConnection,
  HubConnectionBuilder,
} from '@microsoft/signalr';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss'],
})
export class ChatComponent implements OnInit {
  connection: HubConnection | undefined;
  messages: MessageModel[] = [];
  messageInput = '';
  user: AuthResponse | null = null;

  constructor(private auth: AuthService) {}
  ngOnInit(): void {
    this.auth.$user.subscribe((res) => {
      this.user = res;
      this.connection = new HubConnectionBuilder()
        .withUrl(environment.hub + 'chats', {
          accessTokenFactory: () => this.user!.token,
          skipNegotiation: true,
          transport: HttpTransportType.WebSockets,
        })
        .build();

      this.connection.on('message', (x: MessageModel[]) => {
        this.messages = x;
      });
      this.connection
        .start()
        .then(() => {})
        .catch((err) => {
          console.log(err);
        });
    });
  }

  sendMessage() {
    const message: MessageModel = {
      userName: this.user?.userName,
      messageText: this.messageInput,
    };
    this.connection!.invoke('sendMessage', message);
  }
}
