import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplierRequestedOrdersComponent } from './supplier-requested-orders.component';

describe('SupplierRequestedOrdersComponent', () => {
  let component: SupplierRequestedOrdersComponent;
  let fixture: ComponentFixture<SupplierRequestedOrdersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SupplierRequestedOrdersComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SupplierRequestedOrdersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
