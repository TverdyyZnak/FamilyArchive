import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations'

export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }), provideRouter(routes), provideAnimations(),
    
    provideHttpClient(withInterceptors([
      (req, next) => {
        const cloned = req.clone({withCredentials:true})
        return next(cloned)
      }
    ])
  )
  ]
};
