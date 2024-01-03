import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OwnerProductComponent } from './owner-product.component';

describe('OwnerProductComponent', () => {
  let component: OwnerProductComponent;
  let fixture: ComponentFixture<OwnerProductComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OwnerProductComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OwnerProductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
