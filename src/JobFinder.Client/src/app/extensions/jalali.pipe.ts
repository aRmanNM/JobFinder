import { Pipe, PipeTransform } from '@angular/core';
import dayjs from 'dayjs';
import jalaliday from 'jalaliday';

dayjs.extend(jalaliday);

@Pipe({
  name: 'jalaliDate',
})
export class JalaliDatePipe implements PipeTransform {
  // 2025-11-27T16:45:35.705905+00:00
  transform(value: string | Date, format: string = 'HH:mm YYYY/MM/DD'): string {
    if (!value) return '';

    // Ensure UTC conversion, then convert to Jalali
    return dayjs(value).calendar('jalali').locale('fa').format(format);
  }
}
