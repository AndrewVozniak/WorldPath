import { Component } from '@angular/core';

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

  constructor() {
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

    if (this.hoursToDisplay === 24) {
      const now = new Date();
      const limitTime = new Date(now.getTime() + this.hoursToDisplay * 60 * 60 * 1000); // учитываем часы

      const initialFiltered = this.forecast.filter((entry: any) => {
        const entryDate = new Date(entry.date);
        return entryDate <= limitTime;
      });

      if (this.currentPage) {
        const startDate = new Date();
        startDate.setDate(startDate.getDate() + this.currentPage - 1);
        const endDate = new Date(startDate);
        endDate.setDate(endDate.getDate() + 1);

        this.filteredForecast = initialFiltered.filter((entry: any) => {
          const entryDate = new Date(entry.date);
          return entryDate >= startDate && entryDate < endDate;
        });
      } else {
        this.filteredForecast = initialFiltered;
      }
    } else {
      const now = new Date();
      const limitTime = new Date(now.getTime() + this.hoursToDisplay * 60 * 60 * 1000); // учитываем часы

      const initialFiltered = this.forecast.filter((entry: any) => {
        const entryDate = new Date(entry.date);
        return entryDate <= limitTime;
      });

      if (this.currentPage !== undefined) { // Использую !== undefined для ясности
        const startDate = new Date();
        startDate.setDate(startDate.getDate() + this.currentPage - 1);
        startDate.setHours(0, 0, 0, 0); // Устанавливаем начало дня

        const endDate = new Date(startDate);
        endDate.setDate(endDate.getDate() + 1);

        this.filteredForecast = initialFiltered.filter((entry: any) => {
          const entryDate = new Date(entry.date);
          return entryDate >= startDate && entryDate < endDate;
        });
      } else {
        this.filteredForecast = initialFiltered;
      }    }
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

