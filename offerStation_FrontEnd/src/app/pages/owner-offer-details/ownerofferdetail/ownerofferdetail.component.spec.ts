import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OwnerofferdetailComponent } from './ownerofferdetail.component';

describe('OwnerofferdetailComponent', () => {
  let component: OwnerofferdetailComponent;
  let fixture: ComponentFixture<OwnerofferdetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OwnerofferdetailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OwnerofferdetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
