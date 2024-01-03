import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminConfirmOwnercustomerOrderComponent } from './admin-confirm-ownercustomer-order.component';

describe('AdminConfirmOwnercustomerOrderComponent', () => {
  let component: AdminConfirmOwnercustomerOrderComponent;
  let fixture: ComponentFixture<AdminConfirmOwnercustomerOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminConfirmOwnercustomerOrderComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminConfirmOwnercustomerOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
