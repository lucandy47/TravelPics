import { Location } from "./location";

export interface NewPost{
    location: Location;
    description: string;
    photos: File[];
    createdById: number;
  }