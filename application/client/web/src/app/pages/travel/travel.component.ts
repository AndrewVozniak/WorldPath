import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import axios from "axios";
import {environment} from "../../../environments/environment";
// id
// text
// updated_at
// created_at
// user
//     id
//     name
//     profile_photo_path
interface User {
    id?: string;
    name?: string;
    profile_photo_path?: string;
}

interface Comment {
    id?: string;
    text?: string;
    updated_at?: string;
    created_at?: string;
    user?: User;
}

class Travel {
  id?: string;
  title?: string;
  description?: string;
  photos?: string[];
  waypoints?: string[];
  comments?: Comment[];
  places?: string[];
  routes?: string[];
  updated_at?: string;
  created_at?: string;
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
        travel.comments = response.data;

        if (travel.comments) {
            travel.comments.forEach((comment) => {
                if (comment.created_at) {
                    comment.created_at = comment.created_at.split(' ')[0].split('-').reverse().join('.');
                }
            });
        }
      })
      .catch((error) => {
        console.log(error);
      });
  }

  getId(travel: Travel) {
    const id = this.route.snapshot.paramMap.get('id')

    if (id) {
      travel.id = id;
    } else {
      console.log('No id was passed to the route');
    }
  }
}
