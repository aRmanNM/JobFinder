import { Component, OnDestroy, OnInit } from '@angular/core';

import { InputTextModule } from 'primeng/inputtext';
import { TabsModule } from 'primeng/tabs';
import { ButtonModule } from 'primeng/button';
import { MultiSelectModule } from 'primeng/multiselect';
import { CheckboxModule } from 'primeng/checkbox';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SearchSource } from '../interfaces/search-source';
import { AppService } from '../app.service';
import { AccordionModule } from 'primeng/accordion';
import { CardModule } from 'primeng/card';
import { DialogModule } from 'primeng/dialog';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { JobAd } from '../interfaces/job-ad';
import { NavigationEnd, Router } from '@angular/router';
import { firstValueFrom, Subscription } from 'rxjs';
import { Bookmark } from '../interfaces/bookmark';

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

    this.sub = this.router.events.subscribe(async (event) => {
      if (event instanceof NavigationEnd) {
        const bookmarks = await this.getBookmarks();
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
    let bookmarks: JobAd[] = [];
    this.isLoading = true;
    this.activeTab = null;
    this.showTabs = false;
    this.sourcesSnapshot = this.sources.filter((s) => s.isEnabled == true);
    this.appService
      .getAds(this.sourcesSnapshot.map(ss => ss.title), this.query, 1)
      .subscribe(async res => {
        this.isLoading = false;
        if (this.appService.loggedIn()) {
          bookmarks = await this.getBookmarks();
        }

        res.forEach((source) => {

          if (this.appService.loggedIn()) {
            source.ads.forEach(ad => {
              if (bookmarks.find((b) => b.id == ad.id)) {
                ad.bookmarked = true;
              }
            })
          }

          let sourceSnapshot = this.sourcesSnapshot.find(ss => ss.title == source.serviceName);
          if (sourceSnapshot) {
            sourceSnapshot.ads = source.ads;
            sourceSnapshot.pageNumber = 1; // reset page number
            if (this.activeTab == null) {
              this.activeTab = source.serviceName;
            }
          }
        });


        this.showTabs = true;
      });
  }

  loadMore(serviceName: string, query: string) {
    let bookmarks: JobAd[] = [];
    this.isLoading = true;
    let source = this.sourcesSnapshot.find((s) => s.title == serviceName);
    if (source == null) {
      return;
    } else {
      source.pageNumber++;
      this.appService
        .getAds([serviceName], query, source.pageNumber)
        .subscribe(async res => {
          this.isLoading = false;

          if (this.appService.loggedIn()) {
            bookmarks = await this.getBookmarks();
          }

          res.forEach((source) => {

            if (this.appService.loggedIn()) {
              source.ads.forEach(ad => {
                if (bookmarks.find((b) => b.id == ad.id)) {
                  ad.bookmarked = true;
                }
              })
            }

            let sourceSnapshot = this.sourcesSnapshot.find(ss => ss.title == source.serviceName);
            if (sourceSnapshot) {
              sourceSnapshot.ads.push(...source.ads);
            }
          });
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

  bookmarkAd(ad: JobAd, note: string = "") {
    const bookmark: Bookmark = {
      id: 0,
      userId: "",
      content: ad,
      note: note,
      createdAt: null,
      lastEditAt: null,
    }

    this.appService.addBookmark(bookmark).subscribe(res => {
      ad.bookmarked = true;
    });
  }

  async getBookmarks(): Promise<JobAd[]> {
    const bookmarks = await firstValueFrom(this.appService.getBookmarks());
    return bookmarks.map(b => b.content);
  }
}
