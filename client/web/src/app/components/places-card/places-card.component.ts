import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-places-card',
  templateUrl: './places-card.component.html',
  styleUrls: ['./places-card.component.scss']
})
export class PlacesCardComponent {
  @Input() public place?: any;
}
