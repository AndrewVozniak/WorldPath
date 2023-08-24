import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoutesCardComponent } from './routes-card.component';

describe('RoutesCardComponent', () => {
  let component: RoutesCardComponent;
  let fixture: ComponentFixture<RoutesCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RoutesCardComponent]
    });
    fixture = TestBed.createComponent(RoutesCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
