import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminAssignDeliveryOrderComponent } from './admin-assign-delivery-order.component';

describe('AdminAssignDeliveryOrderComponent', () => {
  let component: AdminAssignDeliveryOrderComponent;
  let fixture: ComponentFixture<AdminAssignDeliveryOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminAssignDeliveryOrderComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminAssignDeliveryOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
