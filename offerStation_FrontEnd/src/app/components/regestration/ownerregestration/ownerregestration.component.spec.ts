import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OwnerregestrationComponent } from './ownerregestration.component';

describe('OwnerregestrationComponent', () => {
  let component: OwnerregestrationComponent;
  let fixture: ComponentFixture<OwnerregestrationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OwnerregestrationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OwnerregestrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
