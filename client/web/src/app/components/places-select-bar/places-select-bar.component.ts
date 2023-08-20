import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-places-select-bar',
  templateUrl: './places-select-bar.component.html',
  styleUrls: ['./places-select-bar.component.scss']
})
export class PlacesSelectBarComponent {
  @Output() onSearchChange = new EventEmitter<string[]>();

  public searchValue!: string;

  searchChange(event: any) {
    this.onSearchChange.emit(event);
  }
}
