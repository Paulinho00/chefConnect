import { Injectable } from '@angular/core';
import { fetchAuthSession, fetchUserAttributes } from 'aws-amplify/auth';
import { catchError, from, map, Observable, of, shareReplay, tap } from 'rxjs';
import { UserGroup } from '../models/user-group.model';

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
  private userGroups$: Observable<UserGroup[] | null>;

  constructor() {
    this.userAttributes$ = this.initUserAttributes();
    this.userGroups$ = this.initUserGroups();
  }

  private initUserAttributes(): Observable<User | null> {
    return from(fetchUserAttributes() as Promise<User>).pipe(
      shareReplay(1),
      catchError(() => from([null]))
    );
  }

  private initUserGroups(): Observable<UserGroup[] | null> {
    return from(fetchAuthSession()).pipe(
      shareReplay(1),
      map(
        (authSession) =>
          authSession.tokens?.idToken?.payload['cognito:groups'] as UserGroup[]
      ),
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

  public getUserGroup(): Observable<UserGroup | null> {
    return this.userGroups$.pipe(map((groups) => groups?.[0] ?? null));
  }

  public refreshUserGroups(): Observable<UserGroup[] | null> {
    this.userGroups$ = this.initUserGroups();
    return this.userGroups$;
  }
}
