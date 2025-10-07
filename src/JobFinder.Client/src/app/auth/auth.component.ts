import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { AppService } from '../app.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-auth',
  imports: [CommonModule, ReactiveFormsModule, InputTextModule, PasswordModule, ButtonModule],
  templateUrl: './auth.component.html',
  styles: `
.auth-container {
  // width: 300px;
  margin: 2rem auto;
  padding: 2rem;
  // border: 1px solid #ddd;
  // border-radius: 8px;
  background: #fff;
}

.field {
  margin-bottom: 1rem;
  display: flex;
  flex-direction: column;
}

label {
  margin-bottom: 0.3rem;
}

small {
  color: red;
  font-size: 0.8rem;
}

button {
  width: 100%;
}

.toggle-mode {
  margin-top: 1rem;
  text-align: center;
}

.toggle-mode a {
  cursor: pointer;
  color: #007ad9;
  text-decoration: underline;
}
  `
})
export class AuthComponent {
  isRegisterMode = false; // toggle between login and register

  returnUrl: string | null = '/';

  constructor(private appService: AppService, private route: ActivatedRoute, private router: Router) {
    this.route.queryParamMap.subscribe(params => {
      this.returnUrl = params.get('returnUrl') || '/';
    });
  }

  authForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
    confirmPassword: new FormControl(''), // only used for register
  });

  toggleMode() {
    this.isRegisterMode = !this.isRegisterMode;
    // clear confirm password when switching to login
    if (!this.isRegisterMode) {
      this.authForm.get('confirmPassword')?.reset();
    }
  }

  submit() {
    if (this.authForm.invalid) {
      this.authForm.markAllAsTouched();
      return;
    }

    if (this.isRegisterMode) {
      const { username, password, confirmPassword } = this.authForm.value;
      if (password !== confirmPassword) {
        alert('رمز وارد شده مطاقبت ندارد!');
        return;
      }
      this.appService.register(username, password).subscribe({
        next: () => this.router.navigateByUrl(this.returnUrl || '/'),
        error: err => {
          console.log(err);
          alert("ثبت نام ناموفق، مجددا تلاش کنید");
        },
      });
    } else {
      const { username, password } = this.authForm.value;
      this.appService.login(username, password).subscribe({
        next: () => this.router.navigateByUrl(this.returnUrl || '/'),
        error: err => {
          console.log(err);
          alert("نام کاربری یا رمز عبور اشتباه است");
        },
      });
    }

    this.authForm.reset();
  }
}
