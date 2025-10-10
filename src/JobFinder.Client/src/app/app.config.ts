import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, RouteReuseStrategy } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

import { providePrimeNG } from 'primeng/config';
import Aura from '@primeuix/themes/aura';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { CustomReuseStrategy } from './extensions/reuse-strategy';
import { JwtInterceptor } from './extensions/jwt.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    { provide: RouteReuseStrategy, useClass: CustomReuseStrategy },
    provideAnimationsAsync(),
    provideHttpClient(withInterceptors([JwtInterceptor])),
    providePrimeNG({
      theme: {
        preset: Aura,
      },
    }),
  ],
};
