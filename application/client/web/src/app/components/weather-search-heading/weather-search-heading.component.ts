import {Component, EventEmitter, Output} from '@angular/core';
import {OnInit} from "@angular/core";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-weather-search-heading',
  templateUrl: './weather-search-heading.component.html',
  styleUrls: ['./weather-search-heading.component.scss']
})
export class WeatherSearchHeadingComponent {
  @Output() changeHoursToDisplayEvent = new EventEmitter<number>();
  @Output() forecastEvent = new EventEmitter<any>();

  protected hoursToDisplay: number = 120;
  protected error: string = '';

  protected city: string = '';
  protected lat: string = '';
  protected lon: string = '';


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

    else {
      fetch(`${environment.apiURL}/weather/weather/week?city=${this.city}`, {
        headers: {
          'Content-Type': 'application/json',
        },
      })
        .then(response => response.json())
        .then(response => {
          this.forecastEvent.emit(response);
        })
        .catch(error => {
          console.error('Error fetching data:', error);
          this.error = 'An error occurred while fetching data';
        });
    }
  }

  public searchByCoordinates(): void {
    if(!this.lat || this.lat === '') {
      this.error = 'Latitude are required';
    }

    else if(!/^-?\d+([.,]\d+)?$/.test(this.lat)) {
      this.error = 'Latitude must be a number';
    }

    else if(!this.lon || this.lon === '') {
      this.error = 'Longitude are required';
    }

    else if(!/^-?\d+([.,]\d+)?$/.test(this.lat)) {
      this.error = 'Longitude must be a number';
    }

    else {
      fetch(`${environment.apiURL}/weather/weather/week/coordinates?lat=${this.lat}&lon=${this.lon}`, {
        headers: {
          'Content-Type': 'application/json',
        },
      })
        .then(response => response.json())
        .then(response => {
          this.forecastEvent.emit(response);
        })
        .catch(error => {
          console.error('Error fetching data:', error);
          this.error = 'An error occurred while fetching data';
        });
    }
  }

  public changeHoursToDisplay(hoursToDisplay: number): void {
    this.hoursToDisplay = hoursToDisplay;
    this.changeHoursToDisplayEvent.emit(hoursToDisplay);
  }
}
