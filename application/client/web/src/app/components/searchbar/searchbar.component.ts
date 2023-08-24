import { Component } from '@angular/core';

@Component({
  selector: 'app-searchbar',
  templateUrl: './searchbar.component.html',
  styleUrls: ['./searchbar.component.scss']
})
export class SearchbarComponent {
  public placeholder: string;

  constructor() {
    this.placeholder = 'Start searching...';
  }
}
