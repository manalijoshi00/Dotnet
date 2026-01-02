import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SupplierService {

  private api = `${environment.apiUrl}/Supplier`;

  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get(`${this.api}/GetAll`);
  }

  getById(id: number) {
    return this.http.get(`${this.api}/${id}`);
  }

  add(data: any) {
    return this.http.post(`${this.api}/AddOrUpdate`, data);
  }

  update(id: number, data: any) {
    return this.http.post(`${this.api}/AddOrUpdate?id=${id}`, data);
  }

  delete(id: number) {
    return this.http.delete(`${this.api}/${id}`);
  }
}
