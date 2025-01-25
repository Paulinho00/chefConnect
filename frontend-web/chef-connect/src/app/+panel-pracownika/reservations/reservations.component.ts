import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { RestaurantService } from '../../services/restaurant.service';
import { ReservationService } from '../../services/reservation.service';
import { Restaurant } from '../../models/restaurant.model';
import { Reservation } from '../../models/reservation.model';
import { ReservationTableComponent } from '../components/reservation-table/reservation-table.component';
import { ReservationConfirmationFormComponent } from '../components/reservation-confirmation-form/reservation-confirmation-form.component';
import {
  MatChipListbox,
  MatChipOption,
  MatChipsModule,
} from '@angular/material/chips';
import { Table } from '../../models/table.model';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-reservations',
  imports: [
    ReservationTableComponent,
    ReservationConfirmationFormComponent,
    MatChipsModule,
    MatIconModule,
  ],
  templateUrl: './reservations.component.html',
  styleUrl: './reservations.component.scss',
})
export class ReservationsComponent implements OnInit {
  restaurantService = inject(RestaurantService);
  reservationService = inject(ReservationService);
  restaurants: Restaurant[] = [];
  reservations: Reservation[] = [];
  tables: Table[] = [];
  freeTables: { table: Table; isFree: boolean }[] = [];
  private restaurantId: string = '1e6101b4-4ae2-4c8c-92e2-0a62a3794877';
  selectedReservation: Reservation | null = null;
  @ViewChild(MatChipListbox) chipListbox!: MatChipListbox;
  snackBarRef = inject(MatSnackBar);

  public ngOnInit(): void {
    this.restaurantService.getRestaurants().subscribe((restaurants) => {
      this.restaurants = restaurants;
    });

    this.updateReservations();
    this.restaurantService.getTables(this.restaurantId).subscribe((tables) => {
      this.tables = tables;
    });
  }

  updateReservations() {
    this.reservationService
      .getAllUnconfirmedReservationsForRestaurant(this.restaurantId)
      .subscribe((reservations) => {
        this.reservations = reservations;
      });
  }

  onReservationSelected(reservation: Reservation) {
    this.selectedReservation = reservation;
    this.reservationService
      .getAvailableTablesForDate(
        this.restaurantId,
        this.selectedReservation?.date || ''
      )
      .subscribe((tables) => {
        this.freeTables = this.tables.map((table) => ({
          table,
          isFree: tables.map((table) => table.id).includes(table.id),
        }));
      });
  }

  public onSubmit($event: Reservation) {
    const tableIds = (this.chipListbox.selected as MatChipOption[])
      .map((chip) => chip.value)
      .map((value) => value.table.id);
    this.reservationService
      .confirmReservation($event.id, tableIds)
      .subscribe((res) => {
        this.updateReservations();
        const ref = this.snackBarRef.open(res.message);
        setTimeout(() => {
          ref.dismiss();
          this.selectedReservation = null;
          this.freeTables = [];
          this.chipListbox.writeValue([]);
        }, 5000);
      });
  }
}
