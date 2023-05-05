import { CurrentUserPost } from "./current-user-post";
import { Image } from "./image";

export interface User{
    id:number;
    email: string;
    passwordSalt: string;
    passwordHash: string;
    firstName: string;
    lastName: string;
    phone: string | null;
    posts: CurrentUserPost[];
    createdOn:Date;
    profileImage: Image;
  }
  