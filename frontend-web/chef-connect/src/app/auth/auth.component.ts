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
import { fetchUserAttributes } from 'aws-amplify/auth';

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
  authenticator = inject(AuthenticatorService);
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
      const attributes = await fetchUserAttributes();
      this.router.navigate(['/panel-pracownika']);
    } catch (error) {
      console.error('Error fetching user attributes:', error);
      await this.router.navigate(['/']);
    }
  }
}
