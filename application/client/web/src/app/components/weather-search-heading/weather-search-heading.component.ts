import {Component, EventEmitter, Output} from '@angular/core';
import {OnInit} from "@angular/core";

@Component({
  selector: 'app-weather-search-heading',
  templateUrl: './weather-search-heading.component.html',
  styleUrls: ['./weather-search-heading.component.scss']
})
export class WeatherSearchHeadingComponent {
  @Output() changeHoursToDisplayEvent = new EventEmitter<number>();

  public ngOnInit(): void {
    this.changeHoursToDisplay(120);
  }

  public changeHoursToDisplay(hoursToDisplay: number): void {
    this.changeHoursToDisplayEvent.emit(hoursToDisplay);
  }
}
