import { Component, OnInit } from '@angular/core';
import { AppService } from '../app.service';
import { ProfileModel, RecentQuery } from '../interfaces/profile';
import { JsonPipe } from '@angular/common';
import { AvatarModule } from 'primeng/avatar';
import { CardModule } from 'primeng/card';
import { TableModule } from 'primeng/table';
import { JalaliDatePipe } from "../extensions/jalali.pipe";

@Component({
  selector: 'app-profile',
  imports: [JsonPipe, AvatarModule, CardModule, TableModule, JalaliDatePipe],
  templateUrl: './profile.component.html',
  styles: ``,
})
export class ProfileComponent implements OnInit {
  profile: ProfileModel | undefined;
  avatar: any;

  constructor(private appService: AppService) {}
  ngOnInit() {
    this.appService.getProfile().subscribe((p) => {
      let aggregatedQueries: RecentQuery[] = [];

      p.recentQueries = p.recentQueries.sort((a, b) => b.id - a.id);
      p.recentQueries.forEach((recentQuery) => {
        let currentQuery = aggregatedQueries.find(
          (q) => q.query === recentQuery.query
        );
        if (currentQuery) {
          currentQuery.count++;
          // currentQuery.createdAt = recentQuery.createdAt;
        } else {
          recentQuery.count = 1;
          aggregatedQueries.push(recentQuery);
        }
      });

      p.recentQueries = aggregatedQueries;
      // console.log("queries: ", p.recentQueries);
      this.profile = p;

      if (p.pictureUid != null) {
        this.appService.getFile(p.pictureUid).subscribe((f: any) => {
          this.avatar = URL.createObjectURL(f);
        });
      }
    });
  }
}
