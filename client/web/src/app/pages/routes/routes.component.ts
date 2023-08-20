import { Component } from '@angular/core';

@Component({
  selector: 'app-routes',
  templateUrl: './routes.component.html',
  styleUrls: ['./routes.component.scss']
})
export class RoutesComponent {
  public difficultLevels = ['easy', 'medium', 'hard'];
  public routes = [
    {
      id: 1,
      title: 'Petros: fast start',
      description: 'Travel with confidence no matter the weather.\n' +
        '      Create routes adapted to current conditions.\n' +
        '      Stay up to date with most popular destinations',
      level: 'easy',
    },
    {
      id: 2,
      title: 'Petros: fast start',
      description: 'Travel with confidence no matter the weather.\n' +
        '      Create routes adapted to current conditions.\n' +
        '      Stay up to date with most popular destinations',
      level: 'medium',
    },
    {
      id: 3,
      title: 'Petros: fast start',
      description: 'Travel with confidence no matter the weather.\n' +
        '      Create routes adapted to current conditions.\n' +
        '      Stay up to date with most popular destinations',
      level: 'hard',
    },
  ];
  public avaibleRoutes = [...this.routes];

  updateRoutes(selectedLevels: string[]) {
    this.avaibleRoutes = this.routes.filter(route => selectedLevels.includes(route.level));
  }
}
