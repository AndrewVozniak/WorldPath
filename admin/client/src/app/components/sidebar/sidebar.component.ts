import { Component } from '@angular/core';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})

export class SidebarComponent {
  pages = [
    {
      title: 'Dashboard',
      link: '/'
    },
    {
      title: 'Users',
      link: '/users'
    },
    {
      title: 'Community',
      link: '/community'
    },
    {
      title: 'Reviews',
      link: '/reviews'
    },
    {
      title: 'Places',
      link: '/places'
    },
    {
      title: 'Trips',
      link: '/trips'
    },
    {
      title: 'Routes',
      link: '/routes'
    },
    {
      title: 'Weather',
      link: '/weather'
    },
    {
      title: 'Support',
      link: '/support'
    }
  ]
}

