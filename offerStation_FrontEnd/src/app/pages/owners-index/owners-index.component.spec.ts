import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OwnersIndexComponent } from './owners-index.component';

describe('OwnersIndexComponent', () => {
  let component: OwnersIndexComponent;
  let fixture: ComponentFixture<OwnersIndexComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OwnersIndexComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OwnersIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
