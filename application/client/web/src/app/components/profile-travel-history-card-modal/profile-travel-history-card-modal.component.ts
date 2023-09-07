import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import axios from "axios";

@Component({
  selector: 'app-profile-travel-history-card-modal',
  templateUrl: './profile-travel-history-card-modal.component.html',
  styleUrls: ['./profile-travel-history-card-modal.component.scss']
})
export class TravelHistoryCardModalComponent implements OnChanges {
  @Input() travel_id: any;

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['travel_id'] && changes['travel_id'].currentValue) {
      axios

    }
  }

}
