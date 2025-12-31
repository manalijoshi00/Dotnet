// import { Injectable } from '@angular/core';
// import { HttpClient } from '@angular/common/http';
// import { environment } from '../../environments/environment';

// @Injectable({ providedIn: 'root' })
// export class StockService {

//   private api = `${environment.apiUrl}/Stock`;

//   constructor(private http: HttpClient) {}

//   getAll() {
//     return this.http.get<any>(`${this.api}/All`);
//   }

//   addStock(payload: any) {
//     return this.http.post(`${this.api}/Add`, payload);
//   }

//   reduceFIFO(productId: number, quantity: number) {
//     return this.http.post(
//       `${this.api}/ReduceFIFO?productId=${productId}&quantity=${quantity}`,
//       {}
//     );
//   }

//   delete(id: number) {
//     return this.http.delete(`${this.api}/${id}`);
//   }
// }

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class StockService {

  private api = `${environment.apiUrl}/Stock`;

  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<any>(`${this.api}/All`);
  }

  addStock(payload: any) {
    return this.http.post(`${this.api}/Add`, payload);
  }

  reduceFIFO(productId: number, quantity: number) {
    return this.http.post(
      `${this.api}/ReduceFIFO?productId=${productId}&quantity=${quantity}`,
      {}
    );
  }

  delete(id: number) {
    return this.http.delete(`${this.api}/${id}`);
  }
}