import { Component } from '@angular/core';
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-home-weather',
  templateUrl: './home-weather.component.html',
  styleUrls: ['./home-weather.component.scss']
})
export class HomeWeatherComponent {
  city: any;
  country: any;
  weather: any;
  temperature: any;
  pressure: any;
  humidity: any;
  wind: any;

  constructor() {
    this.getUserLocation();
  }

  getUserLocation() {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(position => {
        this.getWeather(position.coords.latitude, position.coords.longitude);
      });
    }
  }

  getWeather(lat: any, lon: any) {
    let url = `${environment.apiURL}/weather/weather/now/coordinates?lat=${lat}&lon=${lon}`;

    fetch(url)
      .then(response => response.json())
      .then(data => {
        this.city = data.name;
        this.country = data.sys.country;
        this.weather = data.weather[0].main;
        this.temperature = data.main.temp;
        this.pressure = data.main.pressure;
        this.humidity = data.main.humidity;
        this.wind = data.wind.speed;


        this.setBackgroundImage();
      });
  }

  setBackgroundImage() {
    let weather = this.weather;
    let widget = document.getElementById('weather-widget');

    if (widget) {
      if (weather == 'Clouds') {
        widget.style.backgroundImage = 'url(assets/img/nature/system_weather_widget/cloud.svg)';
      }

      else if (weather.includes('Rain')) {
        widget.style.backgroundImage = 'url(assets/img/nature/system_weather_widget/rain.svg)';
      }

      else if (weather.includes('Snow')) {
        widget.style.backgroundImage = 'url(assets/img/nature/system_weather_widget/snow.svg)';
      }

      else if (weather.includes('Clear')) {
        widget.style.backgroundImage = 'url(assets/img/nature/system_weather_widget/clear.svg)';
      }

      else {
        widget.style.backgroundImage = 'url(assets/img/nature/system_weather_widget/clear.svg)';
      }
    }
  }
}
