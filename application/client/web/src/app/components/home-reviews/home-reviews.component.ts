import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home-reviews',
  templateUrl: './home-reviews.component.html',
  styleUrls: ['./home-reviews.component.scss']
})
export class HomeReviewsComponent implements OnInit {
  public title: string = 'Reviews';
  public reviews: any[] = [
    {
      id: 1,
      author: {
        img: 'https://unsplash.it/200/200',
        name: 'John Smith',
      },
      rating: 5,
      text: 'Never thought I\'d find a service this intuitive! WorldPath made my road trip to Lapland so much smoother. The real-time weather updates were a lifesaver!',
    },
    {
      id: 2,
      author: {
        img: 'https://unsplash.it/200/200',
        name: 'Jane Doe',
      },
      rating: 4,
      text: 'I\'ve been using WorldPath for a while now and I\'m very happy with it. I\'ve been recommending it to all my friends and family. Keep up the good work!',
    },
    {
      id: 3,
      author: {
        img: 'https://unsplash.it/200/200',
        name: 'John Doe',
      },
      rating: 3,
      text: 'I\'ve been using WorldPath for a while now and I\'m very happy with it. I\'ve been recommending it to all my friends and family. Keep up the good work!',
    },
  ];
  public currentSlide: number = 2;

  ngOnInit() {
    this.startAutoSlide();
  }

  startAutoSlide() {
    setInterval(() => {
      this.nextSlide();
    }, 8000);
  }

  nextSlide() {
    if (this.currentSlide === this.reviews.length) {
      this.currentSlide = 1;
    } else {
      this.currentSlide++;
    }
  }


  toggleReview(number: number) {
    this.currentSlide = number;
  }
}
