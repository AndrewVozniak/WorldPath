import {Component, EventEmitter, Output} from '@angular/core';
import { OnInit } from "@angular/core";

@Component({
  selector: 'app-weather-pagination',
  templateUrl: './weather-pagination.component.html',
  styleUrls: ['./weather-pagination.component.scss']
})
export class WeatherPaginationComponent {
  @Output() changePageEvent = new EventEmitter<number>();
  protected currentPage: number;
  protected pages: number[];

  constructor() {
    this.currentPage = 1;
    this.pages = Array.from(Array(5).keys());
  }


  changePage(number: number) {
    this.currentPage = number;
    this.changePageEvent.emit(number);
  }
}
