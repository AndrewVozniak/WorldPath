import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlacesSelectBarComponent } from './places-select-bar.component';

describe('PlacesSelectBarComponent', () => {
  let component: PlacesSelectBarComponent;
  let fixture: ComponentFixture<PlacesSelectBarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PlacesSelectBarComponent]
    });
    fixture = TestBed.createComponent(PlacesSelectBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
