import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatToolbar } from '@angular/material/toolbar';
import { NavBarComponent } from '../components/nav-bar/nav-bar.component';

@Component({
  selector: 'app-main-panel',
  imports: [NavBarComponent],
  templateUrl: './main-panel.component.html',
  styleUrl: './main-panel.component.scss',
})
export class MainPanelComponent {}
