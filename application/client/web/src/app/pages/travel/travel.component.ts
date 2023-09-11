import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import axios from "axios";
import {environment} from "../../../environments/environment";

class Travel {
  id?: string;
  title?: string;
  description?: string;
  photos?: string[];
  waypoints?: string[];
  comments?: string[];
  places?: string[];
  routes?: string[];
  updatedAt?: string;
  createdAt?: string;
}

@Component({
  selector: 'app-travel',
  templateUrl: './travel.component.html',
  styleUrls: ['./travel.component.scss']
})

export class TravelComponent {
  public travel?: Travel;
  private routes_id?: string[];
  private places_id?: string[];

  constructor(private readonly route: ActivatedRoute) {
    this.travel = new Travel();
    this.getId(this.travel);
    this.getTravelBaseInfo(this.travel);
    this.getTravelComments(this.travel)
  }


  getTravelBaseInfo(travel: Travel) {
    axios.get(`${environment.apiURL}/travels/travel_service/travel/${travel.id}`)
      .then((response) => {
        console.log(response.data);
        travel.title = response.data.title;
        travel.description = response.data.description;

        this.routes_id = response.data.routes;
        this.places_id = response.data.places;
      })
      .catch((error) => {
        console.log(error);
      });
  }

  getTravelComments(travel: Travel) {
    axios.get(`${environment.apiURL}/travels/travel_service/travel/${travel.id}/comments`)
      .then((response) => {
        console.log(response.data);
        travel.comments = response.data;
      })
      .catch((error) => {
        console.log(error);
      });
  }

  getId(travel: Travel) {
    const id = this.route.snapshot.paramMap.get('id');

    if (id) {
      travel.id = id;
    } else {
      console.log('No id was passed to the route');
    }
  }
}
