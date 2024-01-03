import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OwnerOffersFilterComponent } from './owner-offers-filter.component';

describe('OwnerOffersFilterComponent', () => {
  let component: OwnerOffersFilterComponent;
  let fixture: ComponentFixture<OwnerOffersFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OwnerOffersFilterComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OwnerOffersFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
