import { Component, ViewEncapsulation } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TabMenuModule } from 'primeng/tabmenu';

@Component({
  selector: 'app-bottom-navbar',
  imports: [TabMenuModule, RouterModule],
  templateUrl: './bottom-navbar.component.html',
  encapsulation: ViewEncapsulation.None,
  styles: [`
    .bottom-navbar {
      position: fixed;
      bottom: 0;
      left: 0;
      right: 0;
      height: 56px;
      background-color: #fff;
      border-top: 1px solid #ddd;
      display: flex;
      justify-content: space-around;
      align-items: center;
      z-index: 1000;
    }

    .nav-item {
      display: flex;
      flex-direction: column;
      align-items: center;
      text-decoration: none;
      color: #555;
      font-size: 12px;
    }

    .nav-item i {
      font-size: 1.4rem;
    }

    .nav-item.active {
      color: #007ad9; /* PrimeNG primary color */
    }
  `]
})
export class BottomNavbarComponent {

  constructor() {}
}
