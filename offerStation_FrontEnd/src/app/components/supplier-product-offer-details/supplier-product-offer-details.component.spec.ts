import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplierProductOfferDetailsComponent } from './supplier-product-offer-details.component';

describe('SupplierProductOfferDetailsComponent', () => {
  let component: SupplierProductOfferDetailsComponent;
  let fixture: ComponentFixture<SupplierProductOfferDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SupplierProductOfferDetailsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SupplierProductOfferDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
