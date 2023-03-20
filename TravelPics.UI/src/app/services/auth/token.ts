import * as moment from 'moment';

export interface UserToken{
  accessToken: string;
  expiresOn: moment.Moment;
}
