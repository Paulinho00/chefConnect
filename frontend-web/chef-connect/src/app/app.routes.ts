import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthComponent } from './auth/auth.component';
import { MainPanelComponent } from './main-panel/main-panel.component';
import { groupGuard } from './guards/group.guard';
import { ReservationsComponent } from './+panel-pracownika/reservations/reservations.component';
import { RestaurantsComponent } from './+panel-administratora/restaurants/restaurants.component';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'auth',
    component: AuthComponent,
  },
  {
    path: 'panel-administratora',
    component: MainPanelComponent,
    canActivate: [groupGuard],
    data: { allowedGroups: ['admin'] },
    children: [
      {
        path: 'restauracje',
        component: RestaurantsComponent,
      },
    ],
  },
  {
    path: 'panel-managera',
    component: MainPanelComponent,
    canActivate: [groupGuard],
    data: { allowedGroups: ['admin', 'manager'] },
  },
  {
    path: 'panel-pracownika',
    component: MainPanelComponent,
    canActivate: [groupGuard],
    data: { allowedGroups: ['admin', 'manager', 'pracownik-restauracji'] },
    children: [
      {
        path: 'rezerwacje',
        component: ReservationsComponent,
      },
    ],
  },
];
