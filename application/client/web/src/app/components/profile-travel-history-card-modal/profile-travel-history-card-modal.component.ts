import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import axios from "axios";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-profile-travel-history-card-modal',
  templateUrl: './profile-travel-history-card-modal.component.html',
  styleUrls: ['./profile-travel-history-card-modal.component.scss']
})
export class TravelHistoryCardModalComponent implements OnChanges {
  @Input() travel_id: any;

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['travel_id'] && changes['travel_id'].currentValue) {
      axios.get(`${environment.apiURL}/travelS/${this.travel_id}`).then((res) => {
        console.log(res.data);
      }
      ).catch((err) => {
        console.log(err);
      });
    }
  }
}
