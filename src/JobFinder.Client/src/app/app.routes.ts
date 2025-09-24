import { Routes } from '@angular/router';
import { SearchComponent } from './search/search.component';
import { BookmarksComponent } from './bookmarks/bookmarks.component';

export const routes: Routes = [
  {
    path: 'search',
    component: SearchComponent,
    data: { reuse: true }
  },
  {
    path: 'bookmarks',
    component: BookmarksComponent,
  },
  {
    path: '**',
    component: SearchComponent,
  },
];
