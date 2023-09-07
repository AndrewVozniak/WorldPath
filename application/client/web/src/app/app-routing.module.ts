import { NgModule } from "@angular/core";
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from "./pages/login/login.component";
import { RegisterComponent } from "./pages/register/register.component";
import { HomeComponent } from "./pages/home/home.component";
import { RoutesComponent } from "./pages/routes/routes.component";
import { PlacesComponent } from "./pages/places/places.component";
import { WeatherComponent } from "./pages/weather/weather.component";
import { ProfileComponent } from "./pages/profile/profile.component";

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'routes', component: RoutesComponent },
  { path: 'places', component: PlacesComponent },
  { path: 'travels', component: RoutesComponent },
  { path: 'weather', component: WeatherComponent },
  { path: 'profile', component: ProfileComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {useHash: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
