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
    localStorage.setItem('username', username);
    this.authSubject.next(true);
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('username');
    this.authSubject.next(false);
  }
}
