import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeatherPaginationComponent } from './weather-pagination.component';

describe('WeatherPaginationComponent', () => {
  let component: WeatherPaginationComponent;
  let fixture: ComponentFixture<WeatherPaginationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [WeatherPaginationComponent]
    });
    fixture = TestBed.createComponent(WeatherPaginationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
