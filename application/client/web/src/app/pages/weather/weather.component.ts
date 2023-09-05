import { Component } from '@angular/core';


@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',
  styleUrls: ['./weather.component.scss']
})
export class WeatherComponent {
  public hoursToDisplay?: number;
  public currentPage?: number;

  changedPage($event: number) {
    this.currentPage = $event;
  }

  changeHoursToDisplay($event: number) {
    this.hoursToDisplay = $event;
  }
}
