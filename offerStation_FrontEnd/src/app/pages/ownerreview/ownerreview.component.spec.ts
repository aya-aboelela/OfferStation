import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OwnerreviewComponent } from './ownerreview.component';

describe('OwnerreviewComponent', () => {
  let component: OwnerreviewComponent;
  let fixture: ComponentFixture<OwnerreviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OwnerreviewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OwnerreviewComponent);
    component = fixture.componentInstance;
    
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
