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
  public loading_travel_history?: boolean;

  getTravelHistory() {
    this.loading_travel_history = true;

    // send axios request to /user/get_travel_history with token in local storage
    axios.get(`${environment.apiURL}/user/get_travel_history`, {
      headers: {
        'Authorization': `${localStorage.getItem('token')}`
      }
    }).then((response) => {
      this.travel_histories = response.data;
      this.loading_travel_history = false;
    }).catch((error) => {
      console.log(error);
      this.loading_travel_history = false;
    });
  };

  constructor() {
    this.getTravelHistory();
  };
}
