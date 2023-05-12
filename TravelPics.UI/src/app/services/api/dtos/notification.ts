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
    sender: UserPostInfo;
    status: NotificationStatusEnum;
}

export interface InAppNotification{
    id: number;
    subject: string;
    generatedOn: Date;
    notificationLog: NotificationLog;
}