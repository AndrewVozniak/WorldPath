import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import {HeaderComponent} from "./components/header/header.component";
import {NgOptimizedImage} from "@angular/common";
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { HomeComponent } from './pages/home/home.component';
import { HomeEntryComponent } from './components/home-entry/home-entry.component';
import { SearchbarComponent } from './components/searchbar/searchbar.component';
import { HomeReviewsComponent } from './components/home-reviews/home-reviews.component';
import { HomeWeatherComponent } from './components/home-weather/home-weather.component';
import { FooterComponent } from './components/footer/footer.component';
import { RoutesComponent } from './pages/routes/routes.component';
import { RoutesDifficultLevelComponent } from './components/routes-difficult-level/routes-difficult-level.component';
import { RoutesCardComponent } from './components/routes-card/routes-card.component';
import { PlacesComponent } from './pages/places/places.component';
import { PlacesSearchBarComponent } from './components/places-search-bar/places-search-bar.component';
import { PlacesCardComponent } from './components/places-card/places-card.component';
import { FormsModule } from '@angular/forms';
import { AuthRegisterCardComponent } from './components/auth-register-card/auth-register-card.component';
import { AuthLoginCardComponent } from './components/auth-login-card/auth-login-card.component';
import { WeatherComponent } from './pages/weather/weather.component';
import { WeatherSearchHeadingComponent } from './components/weather-search-heading/weather-search-heading.component';
import { WeatherPaginationComponent } from './components/weather-pagination/weather-pagination.component';
import { ProfileComponent } from './pages/profile/profile.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    HomeEntryComponent,
    SearchbarComponent,
    HomeReviewsComponent,
    HomeWeatherComponent,
    FooterComponent,
    RoutesComponent,
    RoutesDifficultLevelComponent,
    RoutesCardComponent,
    PlacesComponent,
    PlacesSearchBarComponent,
    PlacesCardComponent,
    AuthRegisterCardComponent,
    AuthLoginCardComponent,
    WeatherComponent,
    WeatherSearchHeadingComponent,
    WeatherPaginationComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgOptimizedImage,
    FormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
