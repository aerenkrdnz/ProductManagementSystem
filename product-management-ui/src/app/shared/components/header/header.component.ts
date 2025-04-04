import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '../../core/services/auth.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, MatToolbarModule, MatButtonModule, RouterLink],
  template: `
    <mat-toolbar color="primary">
      <span>Product Management</span>
      <span class="spacer"></span>
      <button mat-button *ngIf="!authService.isLoggedIn()" routerLink="/auth/login">Login</button>
      <button mat-button *ngIf="authService.isLoggedIn()" (click)="authService.logout()">Logout</button>
    </mat-toolbar>
  `,
  styles: [`
    .spacer { flex: 1 1 auto; }
  `]
})
export class HeaderComponent {
  constructor(public authService: AuthService) {}
}