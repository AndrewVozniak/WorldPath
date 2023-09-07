import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private authSubject = new BehaviorSubject<boolean>(!!localStorage.getItem('token'));
  auth$ = this.authSubject.asObservable();

  login(token: string, username: string) {
    localStorage.setItem('token', token);
    localStorage.setItem('user', JSON.stringify({name: username, full_info: false}));
    this.authSubject.next(true);
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authSubject.next(false);
  }
}
