import { Component, OnInit } from '@angular/core';

import { InputTextModule } from 'primeng/inputtext';
import { TabsModule } from 'primeng/tabs';
import { ButtonModule } from 'primeng/button';
import { MultiSelectModule } from 'primeng/multiselect';
import { CheckboxModule } from 'primeng/checkbox';
import { FormsModule } from '@angular/forms';
import { CommonModule, JsonPipe } from '@angular/common';
import { SearchSource } from '../searchSource';
import { AppService } from '../app.service';
import { AccordionModule } from 'primeng/accordion';
import { CardModule } from 'primeng/card';
import { DialogModule } from 'primeng/dialog';
import { JobAd } from '../jobAd';

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
  ],
  providers: [AppService],
  templateUrl: './search.component.html',
})
export class SearchComponent implements OnInit {
  constructor(private appService: AppService) {}

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
  }

  getAds() {
    this.activeTab = null;
    this.showTabs = false;
    this.sourcesSnapshot = this.sources.filter((s) => s.isEnabled == true);
    this.sourcesSnapshot.forEach((s) => {
      s.pageNumber = 1; // reset page number
      this.appService
        .getAds(s.title, this.query, s.pageNumber)
        .subscribe((res: JobAd[]) => {
          if (this.activeTab == null) {
            this.activeTab = s.title;
          }
          this.showTabs = true;
          s.ads = res;
        });
    });
  }

  loadMore(serviceName: string, query: string) {
    let source = this.sourcesSnapshot.find((s) => s.title == serviceName);
    if (source == null) {
      return;
    } else {
      source.pageNumber++;
      this.appService
        .getAds(serviceName, query, source.pageNumber)
        .subscribe((res: JobAd[]) => {
          source.ads.push(...res);
        });
    }
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
}
