import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RestaurantTableComponent } from './restaurant-table.component';

describe('RestaurantTableComponent', () => {
  let component: RestaurantTableComponent;
  let fixture: ComponentFixture<RestaurantTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RestaurantTableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RestaurantTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
