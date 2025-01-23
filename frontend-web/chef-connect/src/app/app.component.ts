import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { RestaurantService } from './services/restaurant.service';
import { ReservationService } from './services/reservation.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  title = 'chef-connect';
  restaurantService = inject(RestaurantService);
  reservationService = inject(ReservationService);
  ngOnInit(): void {
    this.restaurantService.getRestaurants().subscribe((restaurants) => {
      console.log(restaurants);
    });
    this.reservationService
      .getAllUnconfirmedReservationsForRestaurant(
        '1e6101b4-4ae2-4c8c-92e2-0a62a3794877'
      )
      .subscribe((restaurants) => {
        console.log(restaurants);
      });

    this.reservationService
      .getAvailableTablesForDate(
        '1e6101b4-4ae2-4c8c-92e2-0a62a3794877',
        '2025-01-09T16:00:00'
      )
      .subscribe((restaurants) => {
        console.log(restaurants);
      });
  }
}
