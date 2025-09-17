import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AppService {

  constructor(private http: HttpClient) { }

  getWeather() {
    return this.http.get("http://localhost:5156/WeatherForecast");
  }
}
