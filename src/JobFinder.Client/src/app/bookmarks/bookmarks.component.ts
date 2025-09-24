import { Component, inject, OnInit } from '@angular/core';
import { AppService } from '../app.service';
import { JobAd } from '../interfaces/job-ad';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';

@Component({
  selector: 'app-bookmarks',
  imports: [CardModule, ButtonModule, DialogModule],
  templateUrl: './bookmarks.component.html',
})
export class BookmarksComponent implements OnInit {
  bookmarks: JobAd[] = [];

  modalVisible: boolean = false;
  modalContent: string = '';
  modalTitle: string | null = null;

  private appService = inject(AppService);

  ngOnInit(): void {
    this.getBookmarks();
  }

  getBookmarks() {
    this.bookmarks = this.appService.getBookmarks();
  }

  showModal(serviceName: string, title: string | null, url: string | null) {
    this.modalContent = '';
    this.modalTitle = '';
    if (url == null) {
      this.modalContent = 'محتوایی پیدا نشد';
      return;
    }

    this.modalVisible = true;
    this.modalTitle = title;
    this.appService.getAdDetail(serviceName, url).subscribe((res) => {
      this.modalContent = res.description;
    });
  }

  removeBookmark(ad: JobAd) {
    this.appService.removeBookmark(ad);
    this.getBookmarks();
  }
}
