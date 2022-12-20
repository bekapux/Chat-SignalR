import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  loginForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
    ]),
    rememberMe: new FormControl(false),
  });

  constructor(private auth: AuthService, private router: Router) {}

  onSubmit() {
    const sub = this.auth
      .login({
        email: this.loginForm.value.email,
        password: this.loginForm.value.password,
        rememberMe: this.loginForm.value.rememberMe,
      })
      .subscribe(
        (res) => {
          console.log(res);
          sub.unsubscribe();
          this.router.navigateByUrl('chat')
        },
        (err) => {
          console.log(err);
          sub.unsubscribe();
        }
      );
  }
}
