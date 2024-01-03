import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OwnerRequestedOrdersTabsComponent } from './owner-requested-orders-tabs.component';

describe('OwnerRequestedOrdersTabsComponent', () => {
  let component: OwnerRequestedOrdersTabsComponent;
  let fixture: ComponentFixture<OwnerRequestedOrdersTabsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OwnerRequestedOrdersTabsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OwnerRequestedOrdersTabsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
