import { PostDocument } from "./post-document";

export interface Post{
    location: Location;
    description: string;
    photos: PostDocument[];
    createdById: number;
    publishedOn: Date;
}