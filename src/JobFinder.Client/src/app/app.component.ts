import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { MenubarModule } from 'primeng/menubar';
import { BottomNavbarComponent } from "./bottom-navbar/bottom-navbar.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, MenubarModule, BottomNavbarComponent],
  template: `
      <p-menubar>
        <ng-template #start>
          <div class="top-bar">
            <h3>شغل یاب</h3>
            <p>موتور جستجوی مشاغل از وبسایت های کاریابی</p>
          </div>
        </ng-template>
      </p-menubar>
      <div class="my-content">
        <router-outlet class="content-container"></router-outlet>
      </div>
      <app-bottom-navbar></app-bottom-navbar>
  `,
})
export class AppComponent {
  title = 'JobFinder.Client';
}
