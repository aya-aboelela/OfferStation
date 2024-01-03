import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplierofferComponent } from './supplieroffer.component';

describe('SupplierofferComponent', () => {
  let component: SupplierofferComponent;
  let fixture: ComponentFixture<SupplierofferComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SupplierofferComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SupplierofferComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
