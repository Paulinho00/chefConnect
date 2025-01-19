import { Component, inject, OnInit } from '@angular/core';
import { Restaurant } from '../../models/restaurant.model';
import { RestaurantService } from '../../services/restaurant.service';
import { RestaurantTableComponent } from '../components/restaurant-table/restaurant-table.component';

@Component({
  selector: 'app-restaurants',
  imports: [RestaurantTableComponent],
  templateUrl: './restaurants.component.html',
  styleUrl: './restaurants.component.scss',
})
export class RestaurantsComponent implements OnInit {
  restaurantService = inject(RestaurantService);
  restaurants: Restaurant[] = [];

  ngOnInit(): void {
    this.restaurantService.getRestaurants().subscribe((restaurants) => {
      this.restaurants = restaurants;
      console.log(restaurants);
    });
  }
}
