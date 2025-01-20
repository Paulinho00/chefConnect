import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Reservation } from '../../../models/reservation.model';
import { NgClass } from '@angular/common';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';

@Component({
  selector: 'app-reservation-table',
  templateUrl: './reservation-table.component.html',
  styleUrls: ['./reservation-table.component.scss'],
  imports: [
    MatSortModule,
    MatPaginatorModule,
    MatTableModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    NgClass,
    MatRadioModule,
  ],
})
export class ReservationTableComponent {
  @Output() selectionChange = new EventEmitter<Reservation>();
  @Input() set reservations(data: Reservation[]) {
    this.dataSource.data = data;
  }
  displayedColumns: string[] = [
    'select',
    'id',
    'date',
    'numberOfPeople',
    'status',
  ];
  dataSource = new MatTableDataSource<Reservation>();
  selection = new SelectionModel<Reservation>(false, []);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor() {}

  /** Whether the number of selected elements matches the total number of selectable rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.filter(
      (row) => row.reservationStatus === 'UNCONFIRMED'
    ).length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }

    this.dataSource.data
      .filter((row) => row.reservationStatus === 'UNCONFIRMED')
      .forEach((row) => this.selection.select(row));
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.dataSource.sortingDataAccessor = (item, property) => {
      if (property === 'status') {
        return item.reservationStatus; // Ensure this returns a sortable value
      }
      return item[property as 'date'];
    };
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  onSelectionChange(row: Reservation) {
    this.selection.clear();
    this.selection.toggle(row);
    this.selectionChange.emit(row);
  }
}
