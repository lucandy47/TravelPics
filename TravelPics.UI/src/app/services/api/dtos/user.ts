import { CurrentUserPost } from "./current-user-post";

export interface User{
    id:number;
    email: string;
    passwordSalt: string;
    passwordHash: string;
    firstName: string;
    lastName: string;
    phone: string | null;
    posts: CurrentUserPost[];
  }
  