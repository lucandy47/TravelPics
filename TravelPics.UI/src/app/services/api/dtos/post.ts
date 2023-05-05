import { Location } from "./location";
import { Image } from "./image";
import { User } from "./user";

export interface Post{
    location: Location;
    description: string;
    photos: Image[];
    createdById: number;
    publishedOn: Date;
    user: User;
}