import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-image-slider-hidden',
  templateUrl: './image-slider-hidden.component.html',
  styleUrls: ['./image-slider-hidden.component.scss']
})
export class ImageSliderHiddenComponent {
  @Input() images?: string[];

  constructor() {
    console.log(this.images);
  }
}
