import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import axios from "axios";
import {environment} from "../../../environments/environment";

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

interface Photo {
    id?: string;
    path?: string;
    updated_at?: string;
    created_at?: string;
}

class Travel {
  id?: string;
  title?: string;
  description?: string;
  photos?: Photo[];
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
  public comments_is_loading?: boolean = true;

  private routes_id?: string[];
  private places_id?: string[];

  constructor(private readonly route: ActivatedRoute) {
    this.travel = new Travel()
    this.getTravelInfo(this.travel);
  }

  getTravelInfo(travel: Travel) {
    this.getId(travel);
    this.getTravelBaseInfo(travel);
    this.getTravelPhotos(travel);
    this.getTravelComments(travel);
  }

  getId(travel: Travel) {
    const id = this.route.snapshot.paramMap.get('id')

    if (id) {
      travel.id = id;
    } else {
      console.log('No id was passed to the route');
    }
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

  getTravelPhotos(travel: Travel) {
    axios.get(`${environment.apiURL}/travels/travel_service/travels/${travel.id}/photos`)
      .then((response) => {
        travel.photos = response.data;

        if (travel.photos) {
            travel.photos.forEach((photo) => {
                if (photo.created_at) {
                    photo.created_at = photo.created_at.split(' ')[0].split('-').reverse().join('.');
                }
            });
        }
      })
      .catch((error) => {
        console.log(error);
      });
  }

  getTravelComments(travel: Travel) {
    this.comments_is_loading = true;
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
        this.comments_is_loading = false;
      })
      .catch((error) => {
        console.log(error);
        this.comments_is_loading = false;
      });

  }


  protected readonly Travel = Travel;
}
