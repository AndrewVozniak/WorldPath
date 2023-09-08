import { Component, OnInit } from '@angular/core';
import axios from "axios";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-home-reviews',
  templateUrl: './home-reviews.component.html',
  styleUrls: ['./home-reviews.component.scss']
})
export class HomeReviewsComponent implements OnInit {
  public title: string = 'Reviews';
  public reviews?: any[];
  public currentSlide: number = 1;

  getReviews() {
    axios.get(`${environment.apiURL}/reviews/reviews/3`)
      .then((response) => {
        this.reviews = response.data;

        console.log(this.reviews);
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
    if (this.reviews) {
      if (this.currentSlide === this.reviews.length - 1) {
        this.currentSlide = 0;
      } else {
        this.currentSlide++;
      }
    }
  }

  toggleReview(number: number) {
    this.currentSlide = number;
  }
}
