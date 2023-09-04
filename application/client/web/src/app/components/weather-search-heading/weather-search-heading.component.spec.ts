import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeatherSearchHeadingComponent } from './weather-search-heading.component';

describe('WeatherSearchHeadingComponent', () => {
  let component: WeatherSearchHeadingComponent;
  let fixture: ComponentFixture<WeatherSearchHeadingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [WeatherSearchHeadingComponent]
    });
    fixture = TestBed.createComponent(WeatherSearchHeadingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
