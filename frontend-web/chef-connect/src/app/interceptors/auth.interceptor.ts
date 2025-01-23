import { HttpInterceptorFn } from '@angular/common/http';
import { AuthSession, fetchAuthSession } from 'aws-amplify/auth';
import { from, switchMap } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  return from(fetchAuthSession()).pipe(
    switchMap((authSession: AuthSession) => {
      const token = authSession.tokens?.accessToken;
      const authReq = token
        ? req.clone({
            headers: req.headers.set('Authorization', `Bearer ${token}`),
          })
        : req;
      return next(authReq);
    })
  );
};
