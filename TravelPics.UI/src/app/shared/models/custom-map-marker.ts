import { MapPost } from "src/app/services/api/dtos/map-post";
import { Image } from "../../services/api/dtos/image";

export interface CustomMapMarker{
    lat: number;
    long:number;
    photos: Image[];
    icon: string;
    posts: MapPost[];
}