import { Location } from "./location";

export interface Post{
    location: Location;
    description: string;
    photos: File[];
    createdById: number;
  }