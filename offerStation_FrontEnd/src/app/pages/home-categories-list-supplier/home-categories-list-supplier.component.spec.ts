import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeCategoriesListSupplierComponent } from './home-categories-list-supplier.component';

describe('HomeCategoriesListSupplierComponent', () => {
  let component: HomeCategoriesListSupplierComponent;
  let fixture: ComponentFixture<HomeCategoriesListSupplierComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomeCategoriesListSupplierComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HomeCategoriesListSupplierComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
