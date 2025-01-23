import { Component, inject, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';
import {
  AmplifyAuthenticatorModule,
  AuthenticatorService,
} from '@aws-amplify/ui-angular';
import { fetchAuthSession, fetchUserAttributes } from 'aws-amplify/auth';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-auth',
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    AmplifyAuthenticatorModule,
  ],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss',
})
export class AuthComponent implements OnInit {
  private readonly GROUP_ROUTES = {
    admin: '/panel-administratora',
    manager: '/panel-managera',
    'pracownik-restauracji': '/panel-pracownika',
  };
  authenticator = inject(AuthenticatorService);
  authService = inject(AuthService);
  router = inject(Router);

  ngOnInit(): void {
    // Hub snippet could be utilised to mange redirect once
    this.authenticator.subscribe((authenticator) => {
      if (authenticator.authStatus === 'authenticated') {
        this.handleAuthentication();
      }
    });
  }

  async handleAuthentication() {
    try {
      const authSession = await fetchAuthSession();
      this.authService.refreshUser();
      this.authService.refreshUserGroups();
      const userGroups =
        (authSession.tokens?.idToken?.payload['cognito:groups'] as string[]) ||
        [];

      const userGroup = Object.keys(this.GROUP_ROUTES).find((group) =>
        userGroups.includes(group)
      );

      if (userGroup) {
        await this.router.navigate([
          this.GROUP_ROUTES[userGroup as keyof typeof this.GROUP_ROUTES],
        ]);
      } else {
        console.warn('User has no recognized groups');
        await this.router.navigate(['/unauthorized']);
      }
    } catch (error) {
      console.error('Error fetching user attributes:', error);
      await this.router.navigate(['/']);
    }
  }
}
