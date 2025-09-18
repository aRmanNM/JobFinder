import {Component, signal} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {AppService} from './app.service';
import {JsonPipe} from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, JsonPipe],
  providers: [AppService],
  template: `
    <button (click)="getWeather()">get weather</button>
    <h1>{{ weather | json }}</h1>

    <router-outlet/>
  `,
  styles: [],
})
export class App {
  protected readonly title = signal('JobFinder.Client');
  weather: any;

  constructor(private service: AppService) {
  }

  getWeather() {
    this.service.getWeather().subscribe((res) => {
      this.weather = res;
    })
  }
}
