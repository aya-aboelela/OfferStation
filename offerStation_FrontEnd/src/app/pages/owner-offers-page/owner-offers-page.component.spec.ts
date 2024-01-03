import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ownerOffersPageComponent } from './owner-offers-page.component';



describe('ownerOffersPageComponent', () => {
  let component: ownerOffersPageComponent;
  let fixture: ComponentFixture<ownerOffersPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ownerOffersPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ownerOffersPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
