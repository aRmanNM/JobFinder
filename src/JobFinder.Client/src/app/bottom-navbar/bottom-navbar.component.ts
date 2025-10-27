import { Component, ViewEncapsulation } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TabMenuModule } from 'primeng/tabmenu';

@Component({
  selector: 'app-bottom-navbar',
  imports: [TabMenuModule, RouterModule],
  templateUrl: './bottom-navbar.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class BottomNavbarComponent {

  constructor() {}
}
