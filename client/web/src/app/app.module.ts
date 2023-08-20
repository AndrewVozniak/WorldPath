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
  ],
  imports: [
      BrowserModule,
      AppRoutingModule,
      NgOptimizedImage,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
