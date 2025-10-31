import { Component, OnInit } from '@angular/core';
import { AppService } from '../app.service';
import { ProfileModel } from '../interfaces/profile';
import { JsonPipe } from '@angular/common';
import { AvatarModule } from 'primeng/avatar';

@Component({
  selector: 'app-profile',
  imports: [JsonPipe, AvatarModule],
  templateUrl: './profile.component.html',
  styles: ``
})
export class ProfileComponent implements OnInit {
  profile: ProfileModel | undefined;
  avatar: any;

  constructor(private appService: AppService) {
  }
  ngOnInit() {
    this.appService.getProfile().subscribe(p => {
      this.profile = p;

      if (p.pictureUid != null) {
        this.appService.getFile(p.pictureUid).subscribe((f: any) => {
          this.avatar = URL.createObjectURL(f);
        })
      }
    })
  }


}
