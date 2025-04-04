// src/app/modules/auth/login/login.component.ts
import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '../../../core/services/auth.service';
import { RouterLink, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    RouterLink
  ],
  template: `
    <mat-card class="login-card">
      <form [formGroup]="loginForm" (ngSubmit)="onSubmit()">
        <mat-form-field>
          <input matInput formControlName="email" placeholder="Email">
          <mat-error *ngIf="loginForm.get('email')?.hasError('required')">
            Email is required
          </mat-error>
          <mat-error *ngIf="loginForm.get('email')?.hasError('email')">
            Please enter a valid email
          </mat-error>
        </mat-form-field>
        <mat-form-field>
          <input matInput type="password" formControlName="password" placeholder="Password">
          <mat-error *ngIf="loginForm.get('password')?.hasError('required')">
            Password is required
          </mat-error>
        </mat-form-field>
        <button mat-raised-button color="primary" type="submit" [disabled]="loginForm.invalid">Login</button>
        <a mat-button routerLink="/auth/register">Register</a>
      </form>
    </mat-card>
  `,
  styles: [`
    .login-card { 
      max-width: 400px; 
      margin: 2rem auto; 
      padding: 2rem;
      mat-form-field {
        width: 100%;
        margin-bottom: 1rem;
      }
      button {
        margin-right: 1rem;
      }
    }
  `]
})
export class LoginComponent {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  loginForm: FormGroup;

  constructor() {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      const { email, password } = this.loginForm.value;
      this.authService.login({ email, password }).subscribe({
        next: () => this.router.navigate(['/products']),
        error: (err) => console.error('Login failed', err)
      });
    }
  }
}