import {Component, EventEmitter, Output} from '@angular/core';
import {OnInit} from "@angular/core";

@Component({
  selector: 'app-weather-search-heading',
  templateUrl: './weather-search-heading.component.html',
  styleUrls: ['./weather-search-heading.component.scss']
})
export class WeatherSearchHeadingComponent {
  @Output() changeHoursToDisplayEvent = new EventEmitter<number>();

  protected city: string = '';
  protected lat: string = '';
  protected lon: string = '';
  protected error: string = '';

  public ngOnInit(): void {
    this.changeHoursToDisplay(120);
  }

  onCityFieldChange(event: any): void {
    this.city = event.target.value;
  }

  onLatFieldChange(event: any): void {
    this.lat = event.target.value;
  }

  onLonFieldChange(event: any): void {
    this.lon = event.target.value;
  }

  public searchByCity(): void {
    if(!this.city) {
      this.error = 'City name is required';
    }
  }

  public searchByCoordinates(): void {
    if(!this.lat) {
      this.error = 'Latitude are required';
    }

    if(!this.lon) {
      this.error = 'Longitude are required';
    }


  }

  public changeHoursToDisplay(hoursToDisplay: number): void {
    this.changeHoursToDisplayEvent.emit(hoursToDisplay);
  }
}
