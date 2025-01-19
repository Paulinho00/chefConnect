import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Restaurant } from '../models/restaurant.model';

@Injectable({
  providedIn: 'root',
})
export class RestaurantService {
  private apiUrl = environment.restaurantsApiUrl + '/restaurants';
  private http = inject(HttpClient);

  /** Get all restaurants */
  getRestaurants(): Observable<Restaurant[]> {
    return this.http.get<Restaurant[]>(`${this.apiUrl}/all`);
  }
}
