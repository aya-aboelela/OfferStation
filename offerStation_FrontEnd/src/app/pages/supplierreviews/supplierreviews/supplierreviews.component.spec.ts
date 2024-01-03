import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplierreviewsComponent } from './supplierreviews.component';

describe('SupplierreviewsComponent', () => {
  let component: SupplierreviewsComponent;
  let fixture: ComponentFixture<SupplierreviewsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SupplierreviewsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SupplierreviewsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
