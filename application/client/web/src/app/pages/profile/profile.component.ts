import { Component } from '@angular/core';
import axios from "axios";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {
  public travel_histories?: any;

  getTravelHistory() {
    // send axios request to /user/get_travel_history with token in local storage
    axios.get(`${environment.apiURL}/user/get_travel_history`, {
      headers: {
        'Authorization': `${localStorage.getItem('token')}`
      }
    }).then((response) => {
      this.travel_histories = response.data;
      console.log(this.travel_histories);
    }).catch((error) => {
      console.log(error);
    });
  };

  constructor() {
    this.getTravelHistory();
  };
}
