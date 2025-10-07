import { Routes } from '@angular/router';
import { SearchComponent } from './search/search.component';
import { BookmarksComponent } from './bookmarks/bookmarks.component';
import { ProfileComponent } from './profile/profile.component';
import { AuthGuard } from './extensions/auth.guard';
import { AuthComponent } from './auth/auth.component';

export const routes: Routes = [
  {
    path: 'search',
    component: SearchComponent,
    data: { reuse: true }
  },
  {
    path: 'bookmarks',
    component: BookmarksComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'auth',
    component: AuthComponent,
  },
  {
    path: '**',
    component: SearchComponent,
  },
];
