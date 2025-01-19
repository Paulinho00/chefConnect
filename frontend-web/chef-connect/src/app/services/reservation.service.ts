import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

export interface MessageResponseDto {
  message: string;
}

export interface Reservation {
  id: string;
  customerName: string;
  date: string;
  status: string;
}

@Injectable({
  providedIn: 'root',
})
export class ReservationService {
  private apiUrl = environment.reservationsApiUrl + '/reservations';
  private http = inject(HttpClient);

  confirmReservation(
    reservationId: string,
    tableIds: string[]
  ): Observable<MessageResponseDto> {
    const url = `${this.apiUrl}/confirm/${reservationId}`;
    return this.http.put<MessageResponseDto>(url, tableIds);
  }

  getAllUnconfirmedReservationsForRestaurant(
    restaurantId: string
  ): Observable<Reservation[]> {
    const url = `${this.apiUrl}/${restaurantId}/unconfirmed`;
    return this.http.get<Reservation[]>(url);
  }

  getUserReservations(): Observable<Reservation[]> {
    const url = `${this.apiUrl}`;
    return this.http.get<Reservation[]>(url);
  }
}
