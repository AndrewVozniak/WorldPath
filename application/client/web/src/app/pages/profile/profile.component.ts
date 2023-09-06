import { Component } from '@angular/core';
import {Router} from "@angular/router";
import axios from "axios";
import { environment } from "../../../environments/environment";


@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {
  public user?: any
  public date?: any
  public verified?: any
  public is_admin?: any
  public location?: any

  getUserLocation() {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(position => {
        axios.get(`https://maps.googleapis.com/maps/api/geocode/json?latlng=${position.coords.latitude},${position.coords.longitude}&key=${environment.googleMapsApiKey}&language=en`)
          .then((response) => {
            let locality, country;
            for (let component of response.data.results[0].address_components) {
              if (component.types.includes('locality')) {
                locality = component.long_name;
              }
              if (component.types.includes('country')) {
                country = component.long_name;
              }
            }
            this.location = `${locality}, ${country}`;
          })
      });
    }
  }

  getUserInfo() {
    axios.get(`${environment.apiURL}/user/user`, {
      headers: {
        'Authorization': `${localStorage.getItem('token')}`
      }
    }).then((response) => {
      this.user = response.data

      this.date = new Date(this.user.created_at.replace(" ", "T"));
      this.date = this.date.toLocaleDateString('en-GB', {
        day: 'numeric',
        month: 'long',
        year: 'numeric'
      });

      console.log(this.user)

      if(this.user.verified) {
        this.verified = 'Verified'
      }
      else {
        this.verified = 'Not Verified'
      }

      if(this.user.is_admin) {
        this.is_admin = 'Administration Team'
      }

      this.getUserLocation()
    }).catch((error) => {
      console.log(error)
    })
  }

  constructor(private router: Router) {
    if(!localStorage.getItem('token')) {
      this.router.navigate(['/login']);
    }

    this.getUserInfo()
  }
}
