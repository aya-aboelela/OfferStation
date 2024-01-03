import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllOwnerComponent } from './all-owner.component';

describe('AllOwnerComponent', () => {
  let component: AllOwnerComponent;
  let fixture: ComponentFixture<AllOwnerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AllOwnerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AllOwnerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
