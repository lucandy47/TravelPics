import { UserFullInfo } from "./user-full-info";
import { UserPostInfo } from "./user-post-info";
export enum NotificationStatusEnum{
    Created = 1,
    InProgress = 2,
    Received = 3,
    Read = 4,
    Failed = 5
}

export interface NotificationLog{
    id: number;
    createdOn: Date;
    receiver: UserPostInfo;
    sender: UserFullInfo;
    status: NotificationStatusEnum;
    postId: number;
}

export interface InAppNotification{
    id: number;
    subject: string;
    generatedOn: Date;
    notificationLog: NotificationLog;
}