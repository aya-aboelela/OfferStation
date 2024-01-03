import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplierregestrationComponent } from './supplierregestration.component';

describe('SupplierregestrationComponent', () => {
  let component: SupplierregestrationComponent;
  let fixture: ComponentFixture<SupplierregestrationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SupplierregestrationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SupplierregestrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
