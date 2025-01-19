import { Injectable } from '@angular/core';
import { fetchUserAttributes } from 'aws-amplify/auth';
import { catchError, from, Observable, of, shareReplay } from 'rxjs';

export type User = {
  email: string;
  given_name: string;
  family_name: string;
};

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private userAttributes$: Observable<User | null>;

  constructor() {
    this.userAttributes$ = this.initUserAttributes();
  }

  private initUserAttributes(): Observable<User | null> {
    return from(fetchUserAttributes() as Promise<User>).pipe(
      shareReplay(1),
      catchError(() => from([null]))
    );
  }

  public getUser(): Observable<User | null> {
    return this.userAttributes$;
  }

  public refreshUser(): Observable<User | null> {
    this.userAttributes$ = this.initUserAttributes();
    return this.userAttributes$;
  }
}
