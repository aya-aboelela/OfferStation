import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplierRequestedOrdersTabsComponent } from './supplier-requested-orders-tabs.component';

describe('SupplierRequestedOrdersTabsComponent', () => {
  let component: SupplierRequestedOrdersTabsComponent;
  let fixture: ComponentFixture<SupplierRequestedOrdersTabsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SupplierRequestedOrdersTabsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SupplierRequestedOrdersTabsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
