import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { map } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const groupGuard: CanActivateFn = (route) => {
  const router = inject(Router);
  return inject(AuthService)
    .getUserGroup()
    .pipe(
      map((group) => {
        if (group && route.data['allowedGroups'].includes(group)) {
          return true;
        }
        router.navigate(['']);
        return false;
      })
    );
};
