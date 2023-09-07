import { Component } from '@angular/core';
import axios from "axios";
import {environment} from "../../../environments/environment";
import {Router} from "@angular/router";

@Component({
  selector: 'app-profile-user-card',
  templateUrl: './profile-user-card.component.html',
  styleUrls: ['./profile-user-card.component.scss']
})
export class ProfileUserCardComponent {
  public user?: any
  public date?: any
  public verified?: any
  public is_admin?: any
  public location?: any

  constructor(private router: Router) {
    if (!localStorage.getItem('token')) {
      this.router.navigate(['/login']);
    }
  }

  async ngOnInit() {
    await this.getUserInfo()
    await this.getUserLocation()
  }

  async getUserLocation() {
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

  async getUserInfo() {
    if (localStorage.getItem('user')) {
      this.user = JSON.parse(localStorage.getItem('user') || '{}')
      console.log(this.user)
    }
    else {
      axios.get(`${environment.apiURL}/user/user`, {
        headers: {
          'Authorization': `${localStorage.getItem('token')}`
        }}).then((response) => {
        this.user = response.data

        localStorage.setItem('user', JSON.stringify(this.user))
      }).catch((error) => {
        console.log(error)
      })
    }

    this.date = new Date(this.user.created_at.replace(" ", "T"));
    this.date = this.date.toLocaleDateString('en-GB', {
      day: 'numeric',
      month: 'long',
      year: 'numeric'
    });

    if(this.user.verified) {
      this.verified = 'Verified'
    }
    else {
      this.verified = 'Not Verified'
    }

    if(this.user.is_admin) {
      this.is_admin = 'Administration Team'
    }
  }
}
