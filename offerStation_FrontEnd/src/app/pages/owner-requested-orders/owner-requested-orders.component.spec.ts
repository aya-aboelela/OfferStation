import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OwnerRequestedOrdersComponent } from './owner-requested-orders.component';

describe('OwnerRequestedOrdersComponent', () => {
  let component: OwnerRequestedOrdersComponent;
  let fixture: ComponentFixture<OwnerRequestedOrdersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OwnerRequestedOrdersComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OwnerRequestedOrdersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
