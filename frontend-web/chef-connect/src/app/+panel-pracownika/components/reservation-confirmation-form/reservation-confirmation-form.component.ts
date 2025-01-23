import { Component, EventEmitter, Input, Output } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Reservation } from '../../../models/reservation.model';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';

@Component({
  selector: 'app-reservation-confirmation-form',
  imports: [
    MatFormFieldModule,
    MatButtonModule,
    MatInputModule,
    MatCardModule,
    MatSelectModule,
    MatDatepickerModule,
    ReactiveFormsModule,
  ],
  templateUrl: './reservation-confirmation-form.component.html',
  styleUrl: './reservation-confirmation-form.component.scss',
})
export class ReservationConfirmationFormComponent {
  @Output() confirm = new EventEmitter<Reservation>();
  @Input() set reservation(value: Reservation | null) {
    this.reservationValue = value;
    console.log(value);
  }
  public reservationValue!: Reservation | null;

  ngOnInit() {}

  onConfirm() {
    this.confirm.emit(this.reservationValue || ({} as Reservation));
  }

  onCancel() {}
}
