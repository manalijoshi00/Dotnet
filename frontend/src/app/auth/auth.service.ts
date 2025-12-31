import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  login(data: any) {
    return this.http.post(`${this.apiUrl}/Auth/login`, data);
  }

  register(data: any) {
    return this.http.post(`${this.apiUrl}/Auth/register`, data);
  }

  saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  getRole(): string {
    if (typeof window === 'undefined') return '';

    const token = localStorage.getItem('token');
    if (!token) return '';

    try {
      const payload = token.split('.')[1];
      const decoded = JSON.parse(atob(payload));

      // ASP.NET usually sends role like this:
      return (
      decoded.role ||
      decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] ||
      ''
    );
    } catch (error) {
      console.error('Invalid JWT Token', error);
      return '';
    }
  }

  logout() {
    localStorage.removeItem('token');
  }

  isLoggedIn(): boolean {
  if (typeof window === 'undefined') return false;
  return !!localStorage.getItem('token');
}
}