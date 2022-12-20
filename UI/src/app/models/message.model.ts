import { UserModel } from './user.model';

export interface MessageModel {
  id?: number;
  messageText?: string;
  isDeleted?: boolean;
  userName?: string;
  User?: UserModel;
  DateSent?: Date;
}
