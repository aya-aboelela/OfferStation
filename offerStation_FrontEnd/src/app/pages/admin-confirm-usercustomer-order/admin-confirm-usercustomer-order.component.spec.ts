import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminConfirmUsercustomerOrderComponent } from './admin-confirm-usercustomer-order.component';

describe('AdminConfirmUsercustomerOrderComponent', () => {
  let component: AdminConfirmUsercustomerOrderComponent;
  let fixture: ComponentFixture<AdminConfirmUsercustomerOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminConfirmUsercustomerOrderComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminConfirmUsercustomerOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
