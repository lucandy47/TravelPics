import { PostDocument } from "./post-document";
import { UserPostInfo } from "./user-post-info";

export interface CurrentUserPost{
    location: Location;
    description: string;
    photos: PostDocument[];
    createdById: number;
    publishedOn: Date;
    user:UserPostInfo;
}