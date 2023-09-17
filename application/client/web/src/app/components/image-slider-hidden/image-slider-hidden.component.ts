import { Component, Input } from '@angular/core';



interface Image {
  id?: string;
  path?: string;
  updated_at?: string;
  created_at?: string;
}

@Component({
  selector: 'app-image-slider-hidden',
  templateUrl: './image-slider-hidden.component.html',
  styleUrls: ['./image-slider-hidden.component.scss']
})

export class ImageSliderHiddenComponent  {
  @Input() images?: Image[];
  public currentSlide: number;
  public isFullScreen: boolean;

  constructor() {
    this.currentSlide = 0;
    this.isFullScreen = false;
    this.autoSlide();
  }

  autoSlide() {
    setInterval(() => {
      if (!this.isFullScreen){
        this.nextSlide();
      }
    }, 6000);
  }

  fullScreenView() {
    this.isFullScreen = !this.isFullScreen;
  }

  nextSlide() {
    if (!this.images) return;

    if(this.currentSlide !== this.images.length - 1) {
      this.currentSlide = this.currentSlide + 1;
    } else {
      this.currentSlide = 0;
    }
  }

  prevSlide() {
    if (!this.images) return;

    if(this.currentSlide !== 0) {
      this.currentSlide = this.currentSlide - 1;
    } else {
      this.currentSlide = this.images.length - 1;
    }
  }

}
