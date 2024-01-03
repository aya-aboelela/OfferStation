import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplierRequestedOrdersAfterShippedComponent } from './supplier-requested-orders-after-shipped.component';

describe('SupplierRequestedOrdersAfterShippedComponent', () => {
  let component: SupplierRequestedOrdersAfterShippedComponent;
  let fixture: ComponentFixture<SupplierRequestedOrdersAfterShippedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SupplierRequestedOrdersAfterShippedComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SupplierRequestedOrdersAfterShippedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
