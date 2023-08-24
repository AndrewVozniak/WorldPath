import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-routes-card',
  templateUrl: './routes-card.component.html',
  styleUrls: ['./routes-card.component.scss']
})
export class RoutesCardComponent {
  @Input() public route: any;
}
