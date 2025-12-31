import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private apiUrl = `${environment.apiUrl}/Order`;

  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get(`${this.apiUrl}/All`);
  }

  getById(id: number) {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  create(data: any) {
    return this.http.post(`${this.apiUrl}/Create`, data);
  }
}


