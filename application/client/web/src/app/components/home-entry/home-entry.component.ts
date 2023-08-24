import { Component } from '@angular/core';

@Component({
  selector: 'app-home-entry',
  templateUrl: './home-entry.component.html',
  styleUrls: ['./home-entry.component.scss']
})
export class HomeEntryComponent {
  public description: string;
  public title: string;

  constructor() {
    this.title = 'WorldPath';
    this.description = 'Travel with confidence no matter the weather.\n' +
      '      Create routes adapted to current conditions.\n' +
      '      Stay up to date with most popular destinations';
  }
}
