import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthRegisterCardComponent } from './auth-register-card.component';

describe('AuthRegisterCardComponent', () => {
  let component: AuthRegisterCardComponent;
  let fixture: ComponentFixture<AuthRegisterCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AuthRegisterCardComponent]
    });
    fixture = TestBed.createComponent(AuthRegisterCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
