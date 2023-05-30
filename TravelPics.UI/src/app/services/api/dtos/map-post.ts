import { Location } from "./location";
import { Image } from "./image";
import { User } from "./user";
import { Like } from "./like";

export interface MapPost{
    id: number;
    location: Location;
    description: string;
    photos: Image[];
    createdById: number;
    publishedOn: Date;
    user: User;
    likes: Like[];
}