import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { JobAd } from './interfaces/job-ad';
import { Bookmark } from './interfaces/bookmark';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class AppService {
  baseUrl: string = 'http://localhost:5156';
  jwtHelper = new JwtHelperService();

  constructor(private http: HttpClient) { }

  getAds(
    serviceName: string,
    query: string,
    pageNumber: number
  ): Observable<JobAd[]> {
    return this.http.get<JobAd[]>(
      this.baseUrl +
      `/Ads/GetList?serviceName=${serviceName}&query=${query}&pageNumber=${pageNumber}`
    );
  }

  getAdDetail(serviceName: string, url: string): Observable<any> {
    return this.http.get<any>(
      this.baseUrl + `/Ads/GetDetail?serviceName=${serviceName}&url=${url}`
    );
  }

  getBookmarks(): Observable<Bookmark[]> {
    return this.http.get<Bookmark[]>(
      this.baseUrl + `/Bookmarks`
    );
  }

  addBookmark(bookmark: Bookmark): Observable<Bookmark> {
    return this.http.post<Bookmark>(
      this.baseUrl + `/Bookmarks`, bookmark
    );
  }

  removeBookmark(id: number) {
    return this.http.delete(
      this.baseUrl + `/Bookmarks?bookmarkId=${id}`
    );
  }

  login(username: any, password: any): Observable<any> {
    return this.http.post<any>(
      this.baseUrl + `/Auth/Signin`, { username, password }
    ).pipe(map(response => {
      localStorage.setItem('token', response.token)
    }));
  }

  register(username: any, password: any): Observable<any> {
    return this.http.post<any>(
      this.baseUrl + `/Auth/Signup`, { username, password }
    ).pipe(map(response => {
      localStorage.setItem('token', response.token)
    }));
  }

  loggedIn(): boolean {
    const token = localStorage.getItem('token');
    if (token) {
      return !this.jwtHelper.isTokenExpired(token);
    }
    return false;
  }
}
