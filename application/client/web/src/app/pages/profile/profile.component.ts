import { Component } from '@angular/core';
import {Router} from "@angular/router";
import axios from "axios";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {
  public user?: any
  public date?: any

  getUserInfo() {
    axios.get(`${environment.apiURL}/user/user`, {
      headers: {
        'Authorization': `${localStorage.getItem('token')}`
      }
    }).then((response) => {
      this.user = response.data
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
