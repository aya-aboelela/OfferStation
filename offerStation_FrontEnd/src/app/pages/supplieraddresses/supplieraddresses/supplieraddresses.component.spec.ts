import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplieraddressesComponent } from './supplieraddresses.component';

describe('SupplieraddressesComponent', () => {
  let component: SupplieraddressesComponent;
  let fixture: ComponentFixture<SupplieraddressesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SupplieraddressesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SupplieraddressesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
