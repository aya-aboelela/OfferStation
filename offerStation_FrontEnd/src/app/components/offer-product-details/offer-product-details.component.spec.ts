import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfferProductDetailsComponent } from './offer-product-details.component';

describe('OfferProductDetailsComponent', () => {
  let component: OfferProductDetailsComponent;
  let fixture: ComponentFixture<OfferProductDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OfferProductDetailsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OfferProductDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
