import { Component, inject, OnInit } from '@angular/core';
import { AppService } from '../app.service';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { Bookmark } from '../interfaces/bookmark';

@Component({
  selector: 'app-bookmarks',
  imports: [CardModule, ButtonModule, DialogModule],
  templateUrl: './bookmarks.component.html',
  styles: `
.screen-centered {
  text-align: center;
  margin-top: 100px;
}
  `
})
export class BookmarksComponent implements OnInit {
  bookmarks: Bookmark[] = [];

  isEmpty: any = undefined;

  modalVisible: boolean = false;
  modalContent: string = '';
  modalTitle: string | null = null;

  private appService = inject(AppService);

  ngOnInit(): void {
    this.getBookmarks();
  }

  getBookmarks() {
    this.appService.getBookmarks().subscribe((res) => {
      this.bookmarks = res;
      if (this.bookmarks.length == 0) {
        this.isEmpty = true;
      }
    }, err => {
      alert("error getting bookmark data");
    });
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

  removeBookmark(id: number) {
    this.isEmpty = undefined;
    this.appService.removeBookmark(id).subscribe(() => {
      this.getBookmarks();
    })
  }
}
