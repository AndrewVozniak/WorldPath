import { Component } from '@angular/core';

@Component({
  selector: 'app-places',
  templateUrl: './places.component.html',
  styleUrls: ['./places.component.scss']
})

export class PlacesComponent {
  public places = [
    {
      id: 1,
      name: 'Dungeon of Lviv',
      city: 'Lviv',
      country: 'Ukraine',
      description: 'Travel with confidence no matter the weather. Create routes adapted to current conditions. Stay up to date with most popular destinations',
      image: '/assets/img/nature/carpatians.svg',
    },
    {
      id: 1,
      name: 'Santa-claus village',
      city: 'Rovaniemi',
      country: 'Finland',
      description: 'Travel with confidence no matter the weather. Create routes adapted to current conditions. Stay up to date with most popular destinations',
      image: 'https://lh3.googleusercontent.com/p/AF1QipO0vzBg9SogmghuGDaXNJH8cGCzYNG3JtGDlTok=s1360-w1360-h1020',
    },
  ];

  public avaiblePlaces = [...this.places];

  updatePlaces(name: any) {
    this.avaiblePlaces = this.places.filter((place) => {
      return place.name.toLowerCase().includes(name.toLowerCase());
    });
  }
}
