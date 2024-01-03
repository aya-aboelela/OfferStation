import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SuppliermainpageComponent } from './suppliermainpage.component';



describe('SuppliermainpageComponent', () => {
  let component: SuppliermainpageComponent;
  let fixture: ComponentFixture<SuppliermainpageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SuppliermainpageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SuppliermainpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
