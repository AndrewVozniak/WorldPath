import { Component } from '@angular/core';
import axios from "axios";
import {Router} from "@angular/router";
import {environment} from "../../../environments/environment";
import {AuthService} from "../../auth.service";

@Component({
  selector: 'app-auth-login-card',
  templateUrl: './auth-login-card.component.html',
  styleUrls: ['./auth-login-card.component.scss']
})
export class AuthLoginCardComponent {
  public title: string = "Sign In"
  public error: string = ""

  public email: any = ""
  public password: any = ""
  public remember: boolean = false

  constructor(private router: Router, private authService: AuthService) {
    if(localStorage.getItem('token')) {
      this.router.navigate(['/'])
    }
  }

  emailChange(value: any): any {
    this.email = value;
  }

  passwordChange(value: any): any {
    this.password = value;
  }

  rememberChange(value: any): any {
    this.remember = value;
  }

  signIn(): any {
    this.error = ""

    if(this.email == "") {
      return this.error = "Please enter your email";
    }

    if(this.password == "") {
      return this.error = "Please enter your password";
    }

    axios.post(`${environment.apiURL}/user/sign_in_by_email`, {
      email: this.email,
      password: this.password
    }).then((res) => {
      if (res.data.error) {
        this.error = res.data.error;
        return;
      }

      this.authService.login(res.data.token, res.data.username);
      this.router.navigate(['/']);
    }).catch((err) => {
      console.log(err);
    });
  }
}
