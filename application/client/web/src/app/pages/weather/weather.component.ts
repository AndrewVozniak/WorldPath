import { Component } from '@angular/core';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',
  styleUrls: ['./weather.component.scss']
})
export class WeatherComponent {
  public hoursToDisplay: number = 120;
  public currentPage?: number;
  public forecast?: any;
  public filteredForecast?: any;

  constructor(private cdr: ChangeDetectorRef) {
    this.currentPage = 1;
  }


  changeHoursToDisplay($event: number) {
    this.hoursToDisplay = $event;
    this.filterForecast();
  }

  showForecast($event: any) {
    this.forecast = $event;
    this.filterForecast();
  }

  changedPage($event: number) {
    this.currentPage = $event;
    this.filterForecast();
  }
  filterForecast() {
    if (!this.forecast) return;

    const now = new Date();
    const limitTime = new Date(now.getTime() + this.hoursToDisplay * 60 * 60 * 1000);

    const initialFiltered = this.forecast.filter((entry: any) => {
      const entryDate = new Date(entry.date);
      return entryDate <= limitTime;
    });

    if (this.currentPage !== undefined) {
      const startDate = new Date();
      startDate.setDate(startDate.getDate() + this.currentPage - 1);
      // Set hours only when hoursToDisplay is not 24
      if (this.hoursToDisplay !== 24) {
        startDate.setHours(0, 0, 0, 0);
      }

      const endDate = new Date(startDate);
      endDate.setDate(endDate.getDate() + 1);

      this.filteredForecast = initialFiltered.filter((entry: any) => {
        const entryDate = new Date(entry.date);
        return entryDate >= startDate && entryDate < endDate;
      });
      this.cdr.detectChanges();
    } else {
      this.filteredForecast = initialFiltered;
      this.cdr.detectChanges();

    }
  }


  formatDate(dateString: string): string {
    const date = new Date(dateString);
    const options: Intl.DateTimeFormatOptions = { weekday: 'long' };
    const day = new Intl.DateTimeFormat('en-US', options).format(date);
    const hour = date.getHours();
    const minute = date.getMinutes().toString().padStart(2, '0');

    return `${day}, ${hour}.${minute}`;
  }
}

