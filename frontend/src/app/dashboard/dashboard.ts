import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router,RouterModule } from '@angular/router';
import { AuthService } from '../auth/auth.service';

@Component({
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard {

  role = '';

  constructor(
    private auth: AuthService,
    private router: Router
  ) {
    this.role = this.auth.getRole();
  }

  logout() {
    this.auth.logout();
    // location.href = '/login';
    this.router.navigate(['/']);
  }
}
