import {Component} from "@angular/core";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})

export class HeaderComponent {
  showMenu = false;
  showSearch = false;

  toggleSearch() {
    this.showSearch = !this.showSearch;
    this.showMenu = false;
  }

  toggleMenu() {
    this.showMenu = !this.showMenu;
    this.showSearch = false;
  }

  mainMenuItems: any[];
  subMenuItems: any[];
  currentYear: number;
  auth: boolean = false;

  constructor() {
    this.mainMenuItems = [
      { name: 'Weather', link: '/weather' },
      { name: 'Hotels', link: '/hotels' },
      { name: 'Trips', link: '/trips'},
      { name: 'Cars', link: '/cars' },
    ];

    this.subMenuItems = [
      { name: 'Make a review', link: '/review' },
      { name: 'About us', link: '/about' },
      { name: 'Terms of use', link: '/terms' },
      { name: 'Privacy policy', link: '/privacy' },
    ];

    this.currentYear = new Date().getFullYear();
  }

  handleOverlayClick(event: MouseEvent) {
    // Получаем элемент, на котором произошел клик
    const targetElement = event.target as HTMLElement;

    // Получаем элемент menu-overlay
    const menuOverlay = document.getElementById('menu-overlay');

    // Проверяем, является ли элемент, на котором произошел клик, menu-overlay
    if (targetElement === menuOverlay) {
      this.toggleMenu(); // Вызываем toggleMenu() только если клик был на menuOverlay
    }
  }
}
