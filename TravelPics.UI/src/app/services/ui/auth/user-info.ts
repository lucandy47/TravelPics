import { UserToken } from "../../auth/token";

export class UserInfo {
  public name: string = '';
  public authorization: UserToken | undefined | null;
}
