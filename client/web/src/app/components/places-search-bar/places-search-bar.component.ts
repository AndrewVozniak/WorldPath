import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-places-search-bar',
  templateUrl: './places-search-bar.component.html',
  styleUrls: ['./places-search-bar.component.scss']
})
export class PlacesSearchBarComponent {
  @Output() onSearchChange = new EventEmitter<string[]>();

  public searchValue!: string;

  searchChange(event: any) {
    this.onSearchChange.emit(event);
  }
}
