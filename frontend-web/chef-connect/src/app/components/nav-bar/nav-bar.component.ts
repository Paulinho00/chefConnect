import { NgFor } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterLink, RouterLinkActive } from '@angular/router';

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
  ],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.scss',
})
export class NavBarComponent {
  navigationItems: NavItem[] = [
    { name: 'Zamówienia', route: '/zamowienia' },
    { name: 'Rezerwacje', route: '/rezerwacje' },
    { name: 'Magazyn', route: '/magazyn' },
  ];

  logout() {
    // This will be implemented when auth service is ready
    console.log('Logout clicked');
  }
}
