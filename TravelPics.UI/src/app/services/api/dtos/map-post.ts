import { Location } from "./location";
import { Image } from "./image";
import { User } from "./user";

export interface MapPost{
    id: number;
    location: Location;
    photos: Image[];
    publishedOn: Date;
    user: User;
}