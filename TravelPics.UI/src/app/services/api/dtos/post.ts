import { Location } from "./location";
import { PostDocument } from "./post-document";
import { User } from "./user";

export interface Post{
    location: Location;
    description: string;
    photos: PostDocument[];
    createdById: number;
    publishedOn: Date;
    user: User;
}