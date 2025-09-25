import { Component, OnDestroy, OnInit } from '@angular/core';

import { InputTextModule } from 'primeng/inputtext';
import { TabsModule } from 'primeng/tabs';
import { ButtonModule } from 'primeng/button';
import { MultiSelectModule } from 'primeng/multiselect';
import { CheckboxModule } from 'primeng/checkbox';
import { FormsModule } from '@angular/forms';
import { CommonModule, JsonPipe } from '@angular/common';
import { SearchSource } from '../interfaces/search-source';
import { AppService } from '../app.service';
import { AccordionModule } from 'primeng/accordion';
import { CardModule } from 'primeng/card';
import { DialogModule } from 'primeng/dialog';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { JobAd } from '../interfaces/job-ad';
import { NavigationEnd, NavigationStart, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-search',
  imports: [
    InputTextModule,
    TabsModule,
    ButtonModule,
    MultiSelectModule,
    FormsModule,
    CheckboxModule,
    AccordionModule,
    CardModule,
    CommonModule,
    DialogModule,
    ProgressSpinnerModule
  ],
  providers: [AppService],
  templateUrl: './search.component.html',
})
export class SearchComponent implements OnInit, OnDestroy {
  constructor(private appService: AppService, private router: Router) { }

  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }

  isLoading = false;
  modalIsLoading = false;

  private sub?: Subscription;

  modalVisible: boolean = false;
  modalContent: string = '';
  modalTitle: string | null = null;

  query: string = '';
  activeTab: any;
  showTabs: boolean = false;
  sourcesSnapshot: SearchSource[] = [];
  sources: SearchSource[] = [
    {
      title: 'Jobinja',
      titleFa: 'جابینجا',
      ads: [],
      isEnabled: true,
      pageNumber: 1,
    },
    {
      title: 'Quera',
      titleFa: 'کوئرا',
      ads: [],
      isEnabled: true,
      pageNumber: 1,
    },
    {
      title: 'Jobvision',
      titleFa: 'جاب‌ویژن',
      ads: [],
      isEnabled: true,
      pageNumber: 1,
    },
  ];

  ngOnInit(): void {
    this.sourcesSnapshot = this.sources;

    this.sub = this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        const bookmarks = this.getBookmarks();
        this.sources.forEach((s) => {
          s.ads.forEach((ad) => {
            if (bookmarks.find((b) => b.id == ad.id)) {
              ad.bookmarked = true;
            } else {
              ad.bookmarked = false;
            }
          })
        });
      }
    });
  }

  getAds() {
    this.isLoading = true;
    this.activeTab = null;
    this.showTabs = false;
    this.sourcesSnapshot = this.sources.filter((s) => s.isEnabled == true);
    this.sourcesSnapshot.forEach((s) => {
      s.pageNumber = 1; // reset page number
      this.appService
        .getAds(s.title, this.query, s.pageNumber)
        .subscribe((res: JobAd[]) => {
          this.isLoading = false;
          const bookmarks = this.getBookmarks();
          res.forEach((ad) => {
            if (bookmarks.find((b) => b.id == ad.id)) {
              ad.bookmarked = true;
            }
          });

          if (this.activeTab == null) {
            this.activeTab = s.title;
          }
          this.showTabs = true;
          s.ads = res;
        });
    });
  }

  loadMore(serviceName: string, query: string) {
    this.isLoading = true;
    let source = this.sourcesSnapshot.find((s) => s.title == serviceName);
    if (source == null) {
      return;
    } else {
      source.pageNumber++;
      this.appService
        .getAds(serviceName, query, source.pageNumber)
        .subscribe((res: JobAd[]) => {
          this.isLoading = false;
          const bookmarks = this.getBookmarks();
          res.forEach((ad) => {
            if (bookmarks.find((b) => b.id == ad.id)) {
              ad.bookmarked = true;
            }
          });

          source.ads.push(...res);
        });
    }
  }

  showModal(serviceName: string, title: string | null, url: string | null) {
    this.modalIsLoading = true;
    this.modalContent = '';
    this.modalTitle = '';
    if (url == null) {
      this.modalContent = 'محتوایی پیدا نشد';
      return;
    }

    this.modalVisible = true;
    this.modalTitle = title;
    this.appService.getAdDetail(serviceName, url).subscribe((res) => {
      this.modalIsLoading = false;
      this.modalContent = res.description;
    });
  }

  bookmarkAd(ad: JobAd) {
    this.appService.addBookmark(ad);
    ad.bookmarked = true;
  }

  getBookmarks(): JobAd[] {
    return this.appService.getBookmarks();
  }
}
