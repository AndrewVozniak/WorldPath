import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthLoginCardComponent } from './auth-login-card.component';

describe('AuthLoginCardComponent', () => {
  let component: AuthLoginCardComponent;
  let fixture: ComponentFixture<AuthLoginCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AuthLoginCardComponent]
    });
    fixture = TestBed.createComponent(AuthLoginCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
