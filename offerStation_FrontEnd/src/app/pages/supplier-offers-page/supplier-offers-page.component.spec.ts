import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplierOffersPageComponent } from './supplier-offers-page.component';

describe('SupplierOffersPageComponent', () => {
  let component: SupplierOffersPageComponent;
  let fixture: ComponentFixture<SupplierOffersPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SupplierOffersPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SupplierOffersPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
