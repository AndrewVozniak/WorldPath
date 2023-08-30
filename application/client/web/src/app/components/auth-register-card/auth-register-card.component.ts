import { Component } from '@angular/core';

@Component({
  selector: 'app-auth-register-card',
  templateUrl: './auth-register-card.component.html',
  styleUrls: ['./auth-register-card.component.scss']
})
export class AuthRegisterCardComponent {
  public title: string = "Sign Up"



  // validateEmail(): any {
  //   if(this.email == "" || this.email == null) {
  //     return this.error = "Please enter your email address";
  //   }
  //
  //   if(this.email.indexOf('@') <= 0) {
  //     return this.error = "Please enter a valid email address";
  //   }
  //
  //   if((this.email.charAt(this.email.length-4) != '.') && (this.email.charAt(this.email.length-3) != '.')) {
  //     return this.error = "Please enter a valid email address";
  //   }
  //
  //   return this.error = "";
  // }
  //
  // validatePassword(): any {
  //   if(this.password == "" || this.password == null) {
  //     return this.error = "Please enter your password";
  //   }
  //
  //   if(this.password.length < 8) {
  //     return this.error = "Password must be at least 8 characters";
  //   }
  //
  //   if(this.password.length > 20) {
  //     return this.error = "Password must be less than 20 characters";
  //   }
  //
  //   if(this.password.search(/[a-z]/i) < 0) {
  //     return this.error = "Password must contain at least one letter";
  //   }
  //
  //   return this.error = "";
  // }
}
