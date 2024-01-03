import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OwnerRequestedOrdersAfterShippedComponent } from './owner-requested-orders-after-shipped.component';

describe('OwnerRequestedOrdersAfterShippedComponent', () => {
  let component: OwnerRequestedOrdersAfterShippedComponent;
  let fixture: ComponentFixture<OwnerRequestedOrdersAfterShippedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OwnerRequestedOrdersAfterShippedComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OwnerRequestedOrdersAfterShippedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
