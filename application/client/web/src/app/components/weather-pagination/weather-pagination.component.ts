import { Component, EventEmitter, Output, Input, OnChanges } from '@angular/core';
import { SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-weather-pagination',
  templateUrl: './weather-pagination.component.html',
  styleUrls: ['./weather-pagination.component.scss']
})
export class WeatherPaginationComponent implements OnChanges {
  @Output() changePageEvent = new EventEmitter<number>();
  @Input() hoursToDisplay?: number; // входной массив данных

  public currentPage?: number;
  public pages: number[] = [];

  ngOnChanges(changes: SimpleChanges) {
    if (changes['hoursToDisplay'] && changes['hoursToDisplay'].currentValue !== changes['hoursToDisplay'].previousValue) {
      this.calculatePages();
      if (this.pages.length && (this.currentPage === undefined || this.currentPage > this.pages.length)) {
        this.setPage(1);
      }
    }
  }


  calculatePages() {
    if(this.hoursToDisplay === undefined) return;

    if(this.hoursToDisplay === 24) {
      this.pages = [1]
    }
    else if(this.hoursToDisplay === 120) {
      this.pages = [1, 2, 3, 4, 5]
    }
  }

  setPage(number: number) {
    this.currentPage = number;
    this.changePageEvent.emit(number);
  }
}
