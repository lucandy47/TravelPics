import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({
  name: 'longDateTime'
})
export class LongDatetimePipe implements PipeTransform {

  transform(value: Date | string, labelForNullValue:string = ''): string {
    if(value !=null){
      return moment.parseZone(value).format('MM/DD/YYYY hh:mm A');
    }
    return labelForNullValue;
  }
}

@Pipe({
  name: 'shortDateTime'
})
export class ShortDatetimePipe implements PipeTransform {

  transform(value: Date | string, labelForNullValue:string = ''): string {
    if(value !=null){
      return moment.parseZone(value).format('MM/DD/YYYY');
    }
    return labelForNullValue;
  }
}
