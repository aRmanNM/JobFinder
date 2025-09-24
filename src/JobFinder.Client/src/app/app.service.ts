import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { JobAd } from './interfaces/job-ad';

@Injectable({
  providedIn: 'root',
})
export class AppService {
  baseUrl: string = 'http://localhost:5156';
  constructor(private http: HttpClient) {}

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

  getBookmarks(): JobAd[] {
    var ads: JobAd[] = JSON.parse(localStorage.getItem('bookamrks') ?? '[]');
    return ads;
  }

  addBookmark(ad: JobAd): JobAd[] {
    let bookmarks = this.getBookmarks();
    if (bookmarks.find((a) => a.id == ad.id) == undefined) {
      bookmarks.push(ad);
      localStorage.setItem('bookamrks', JSON.stringify(bookmarks));
      return bookmarks;
    } else {
      return bookmarks;
    }
  }

  removeBookmark(ad: JobAd): JobAd[] {
    let bookmarks = this.getBookmarks();
    if (bookmarks.find((a) => a.id == ad.id) != undefined) {
      bookmarks = bookmarks.filter((a) => a.id != ad.id);
      localStorage.setItem('bookamrks', JSON.stringify(bookmarks));
      return bookmarks;
    } else {
      return bookmarks;
    }
  }
}
