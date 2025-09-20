import { Component, inject } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { ButtonModule } from 'primeng/button';

import { MenubarModule } from 'primeng/menubar';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, MenubarModule, ButtonModule],
  template: `
    <p-menubar>
      <ng-template #start>
        <div class="top-bar">
          <h3>شغل یاب</h3>
          <p>موتور جستجوی مشاغل از وبسایت های کاریابی</p>
        </div>
      </ng-template>
      <ng-template #end>
        <div class="top-bar-left">
          <p-button
            [label]="label"
            variant="text"
            severity="secondary"
            size="small"
            (onClick)="goToPage()"
          />
        </div>
      </ng-template>
    </p-menubar>
    <router-outlet></router-outlet>
  `,
})
export class AppComponent {
  label: string = 'بوکمارک‌ها';
  title = 'JobFinder.Client';

  private router = inject(Router);

  goToPage() {
    if (this.label == 'بوکمارک‌ها') {
      this.router.navigate(['/bookmarks']);
      this.label = 'جستجو';
    } else if (this.label == 'جستجو') {
      this.router.navigate(['/search']);
      this.label = 'بوکمارک‌ها';
    }
  }
}
