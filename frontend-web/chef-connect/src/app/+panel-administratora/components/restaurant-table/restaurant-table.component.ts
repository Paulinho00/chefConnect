import { Component, ViewChild } from '@angular/core';
import { Input } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { Restaurant } from '../../../models/restaurant.model';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-restaurant-table',
  imports: [
    MatSortModule,
    MatPaginatorModule,
    MatTableModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
  ],
  templateUrl: './restaurant-table.component.html',
  styleUrl: './restaurant-table.component.scss',
})
export class RestaurantTableComponent {
  @Input() set restaurants(data: Restaurant[]) {
    this.dataSource.data = data;
  }

  displayedColumns: string[] = [
    'name',
    'adress',
    'openTime',
    'closeTime',
    'numberOfSeats',
  ];
  dataSource = new MatTableDataSource<Restaurant>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}
