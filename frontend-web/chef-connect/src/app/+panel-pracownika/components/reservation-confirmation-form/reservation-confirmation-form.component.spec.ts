import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationConfirmationFormComponent } from './reservation-confirmation-form.component';

describe('ReservationConfirmationFormComponent', () => {
  let component: ReservationConfirmationFormComponent;
  let fixture: ComponentFixture<ReservationConfirmationFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReservationConfirmationFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReservationConfirmationFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
