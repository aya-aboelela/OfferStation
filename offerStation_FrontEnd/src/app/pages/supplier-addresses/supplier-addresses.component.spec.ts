import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplierAddressesComponent } from './supplier-addresses.component';

describe('SupplierAddressesComponent', () => {
  let component: SupplierAddressesComponent;
  let fixture: ComponentFixture<SupplierAddressesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SupplierAddressesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SupplierAddressesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
