import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterViewComponent } from './register-view.component';

describe('RegisterViewComponent', () => {
  let component: RegisterViewComponent;
  let fixture: ComponentFixture<RegisterViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegisterViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
