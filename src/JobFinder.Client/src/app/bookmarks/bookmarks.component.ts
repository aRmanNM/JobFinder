import { Component, inject, OnInit } from '@angular/core';
import { AppService } from '../app.service';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { Bookmark } from '../interfaces/bookmark';
import { TagModule } from 'primeng/tag';
import { ServiceNamePipe } from '../extensions/service-name.pipe';
import { JalaliDatePipe } from '../extensions/jalali.pipe';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { CommonModule } from '@angular/common';
import { TextareaModule } from 'primeng/textarea';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-bookmarks',
  imports: [
    CardModule,
    ButtonModule,
    DialogModule,
    TagModule,
    ServiceNamePipe,
    JalaliDatePipe,
    ProgressSpinnerModule,
    CommonModule,
    TextareaModule,
    FormsModule,
  ],
  templateUrl: './bookmarks.component.html',
  styles: `
.screen-centered {
  text-align: center;
  margin-top: 100px;
}

.p-textarea {
  width:100%
}

.lastEdit {
  margin-right: 10px;
  color: lightgray;
  font-size: 10px;
}
  `,
})
export class BookmarksComponent implements OnInit {
  bookmarks: Bookmark[] = [];

  isEmpty: any = undefined;

  modalContent: string = '-';
  modalTitle: string = '-';
  descModalVisible: boolean = false;
  noteModalVisible: boolean = false;
  noteLastEdit: string | null = null;

  modalIsLoading = false;

  note: string = '';
  selectedBookmark: Bookmark | null = null;

  private appService = inject(AppService);

  ngOnInit(): void {
    this.getBookmarks();
  }

  getBookmarks() {
    this.appService.getBookmarks().subscribe(
      (res) => {
        // console.log("bookmarks: ", res);
        this.bookmarks = res;
        if (this.bookmarks.length == 0) {
          this.isEmpty = true;
        }
      },
      (err) => {
        alert('error getting bookmark data');
      }
    );
  }

  showDescModal(serviceName: string, title: string, url: string | null) {
    this.modalContent = '';
    this.modalTitle = title;
    if (url == null) {
      this.modalContent = 'محتوایی پیدا نشد';
      return;
    }

    this.descModalVisible = true;

    this.modalIsLoading = true;

    this.appService.getAdDetail(serviceName, url).subscribe((res) => {
      this.modalContent = res.description;
      this.modalIsLoading = false;
    });
  }

  showNoteModal(bookmark: Bookmark) {
    this.selectedBookmark = bookmark;
    this.noteModalVisible = true;
    this.noteLastEdit = bookmark.lastEditAt;
    this.note = bookmark.note ?? '';
  }

  removeBookmark() {
    if (!this.selectedBookmark) {
      return;
    }

    this.isEmpty = undefined;
    this.appService.removeBookmark(this.selectedBookmark.id).subscribe(() => {
      this.noteModalVisible = false;
      this.getBookmarks();
    });
  }

  updateBookmark() {
    if (this.selectedBookmark) {
      this.selectedBookmark.note = this.note;
      this.appService.updateBookmark(this.selectedBookmark).subscribe((res) => {
        this.noteModalVisible = false;
        this.selectedBookmark!.lastEditAt = res.lastEditAt;
      });
    }
  }
}
