import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImageSliderHiddenComponent } from './image-slider-hidden.component';

describe('ImageSliderHiddenComponent', () => {
  let component: ImageSliderHiddenComponent;
  let fixture: ComponentFixture<ImageSliderHiddenComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ImageSliderHiddenComponent]
    });
    fixture = TestBed.createComponent(ImageSliderHiddenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
