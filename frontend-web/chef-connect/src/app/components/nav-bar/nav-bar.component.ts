import { Component, inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { AsyncPipe } from '@angular/common';
import { AuthenticatorService } from '@aws-amplify/ui-angular';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

export interface NavItem {
  name: string;
  route: string;
}

@Component({
  selector: 'app-nav-bar',
  imports: [
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    RouterLink,
    RouterLinkActive,
    AsyncPipe,
    MatProgressSpinnerModule,
  ],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.scss',
})
export class NavBarComponent {
  authService = inject(AuthService);
  authenticator = inject(AuthenticatorService);
  router = inject(Router);
  navigationItems: Record<string, NavItem[]> = {
    admin: [
      { name: 'Pracownicy', route: 'pracownicy' },
      { name: 'Restauracje', route: 'restauracje' },
      { name: 'Użytkownicy', route: 'uzytkownicy' },
    ],
    manager: [],
    'pracownik-restauracji': [
      { name: 'Zamówienia', route: 'zamowienia' },
      { name: 'Rezerwacje', route: 'rezerwacje' },
      { name: 'Magazyn', route: 'magazyn' },
    ],
    '': [],
  };

  public signOut() {
    this.authenticator.signOut();
    this.router.navigate(['/']);
  }
}
