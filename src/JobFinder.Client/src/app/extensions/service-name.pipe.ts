import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'serviceName',
})
export class ServiceNamePipe implements PipeTransform {
  transform(value: string): string {
    switch (value) {
      case 'Jobinja':
        return 'جابینجا';
      case 'Quera':
        return 'کوئرا';
      case 'Jobvision':
        return 'جاب‌ویژن';
      default:
        return '-';
    }
  }
}
