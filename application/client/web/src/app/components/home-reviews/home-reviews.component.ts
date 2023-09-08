import { Component, OnInit } from '@angular/core';
import axios from "axios";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-home-reviews',
  templateUrl: './home-reviews.component.html',
  styleUrls: ['./home-reviews.component.scss']
})
export class HomeReviewsComponent implements OnInit {
  public title: string = 'Review';
  public reviews: any[] = [
    {
      id: 1,
      author: {
        img: 'https://unsplash.it/200/200',
        name: 'John Smith',
      },
      text: 'Never thought I\'d find a service this intuitive! WorldPath made my road trip to Lapland so much smoother. The real-time weather updates were a lifesaver!',
    },
    {
      id: 2,
      author: {
        img: 'https://unsplash.it/200/200',
        name: 'Jane Doe',
      },
      text: 'I\'ve been using WorldPath for a while now and I\'m very happy with it. I\'ve been recommending it to all my friends and family. Keep up the good work!',
    },
    {
      id: 3,
      author: {
        img: 'https://unsplash.it/200/200',
        name: 'John Doe',
      },
      text: 'I\'ve been using WorldPath for a while now and I\'m very happy with it. I\'ve been recommending it to all my friends and family. Keep up the good work!',
    },
  ];
  public currentSlide: number = 2;

  getReviews() {
    axios.get(`${environment.apiURL}/reviews/api/Review/GetAllReviews`)
      .then((response) => {
        this.reviews = response.data;
      })
      .catch((error) => {
        console.log(error);
      });
  }

  constructor() {
    this.getReviews();
  }

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
