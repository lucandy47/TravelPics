import { Image } from "./image";
import { UserPostInfo } from "./user-post-info";

export interface CurrentUserPost{
    location: Location;
    description: string;
    photos: Image[];
    createdById: number;
    publishedOn: Date;
    user:UserPostInfo;
}