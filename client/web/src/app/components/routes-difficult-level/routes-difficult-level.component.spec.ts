import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoutesDifficultLevelComponent } from './routes-difficult-level.component';

describe('RoutesDifficultLevelComponent', () => {
  let component: RoutesDifficultLevelComponent;
  let fixture: ComponentFixture<RoutesDifficultLevelComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RoutesDifficultLevelComponent]
    });
    fixture = TestBed.createComponent(RoutesDifficultLevelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
