import { Component } from '@angular/core';
import {Router} from "@angular/router";
import {AuthService} from "../../auth.service";
import axios from "axios";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-auth-register-card',
  templateUrl: './auth-register-card.component.html',
  styleUrls: ['./auth-register-card.component.scss']
})
export class AuthRegisterCardComponent {
  public title: string = "Sign Up"

  public name: string = "";
  public email: string = "";
  public password: string = "";
  public passwordConfirmation: string = "";
  public privacyPolicy: boolean = false;

  public error?: string;

  constructor(private router: Router, private authService: AuthService) {
    if(localStorage.getItem('token')) {
      this.router.navigate(['/'])
    }
  }

  nameChange(value: any): any {
    this.name = value;
  }

  emailChange(value: any): any {
    this.email = value;
  }

  passwordChange(value: any): any {
    this.password = value;
  }

  passwordConfirmationChange(value: any): any {
    this.passwordConfirmation = value;
  }

  privacyPolicyChange(value: any): any {
    this.privacyPolicy = value;
  }

  validateEmail(): any {
    if(this.email == "" || this.email == null) {
      return this.error = "Please enter your email address";
    }

    if(this.email.indexOf('@') <= 0) {
      return this.error = "Please enter a valid email address";
    }

    if(this.email.indexOf('.') <= 0) {
      return this.error = "Please enter a valid email address";
    }

    return this.error = "";
  }

  validatePassword(): any {
    if(this.password == "" || this.password == null) {
      return this.error = "Please enter your password";
    }

    if(this.password.length < 8) {
      return this.error = "Password must be at least 8 characters";
    }

    if(this.password.length > 20) {
      return this.error = "Password must be less than 20 characters";
    }

    if(this.password.search(/[a-z]/i) < 0) {
      return this.error = "Password must contain at least one letter";
    }

    return this.error = "";
  }

  checkPasswordConfirmation(): any {
    if(this.passwordConfirmation == "" || this.passwordConfirmation == null) {
      return this.error = "Please confirm your password";
    }

    if(this.passwordConfirmation != this.password) {
      return this.error = "Passwords do not match";
    }

    return this.error = "";
  }

  validateName(): any {
    if(this.name == "" || this.name == null) {
      return this.error = "Please enter your name";
    }

    if(this.name.length < 2) {
      return this.error = "Name must be at least 2 characters";
    }

    if(this.name.length > 20) {
      return this.error = "Name must be less than 20 characters";
    }

    if(this.name.search(/[a-z]/i) < 0) {
      return this.error = "Name must contain at least one letter";
    }

    return this.error = "";
  }

  register(): any {
    this.validateEmail();

    if (!this.error) {
      this.validateName();
    }

    if (!this.error) {
      this.validatePassword();
    }

    if (!this.error) {
      this.checkPasswordConfirmation();
    }

    if (!this.error) {
      if (!this.privacyPolicy) {
        return this.error = "Please accept the privacy policy";
      }
    }

    if(!this.error) {
      axios.post(`${environment.apiURL}/user/create_user`, {
        name: this.name,
        email: this.email,
        password: this.password
      }).then((res) => {
        if (res.data.error) {
          this.error = res.data.error;
          return;
        }

        this.authService.login(res.data.token, this.name);
        this.router.navigate(['/']);
      }).catch((err) => {
        console.log(err);
      });
    }
  }
}
