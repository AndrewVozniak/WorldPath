import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlacesSearchBarComponent } from './places-search-bar.component';

describe('PlacesSelectBarComponent', () => {
  let component: PlacesSearchBarComponent;
  let fixture: ComponentFixture<PlacesSearchBarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PlacesSearchBarComponent]
    });
    fixture = TestBed.createComponent(PlacesSearchBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
