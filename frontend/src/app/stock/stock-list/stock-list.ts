// import { Component, OnInit } from '@angular/core';
// import { CommonModule } from '@angular/common';
// import {
//   FormBuilder,
//   FormGroup,
//   ReactiveFormsModule,
//   Validators
// } from '@angular/forms';
// import { StockService } from '../stock.service';

// @Component({
//   selector: 'app-stock-list',
//   standalone: true,
//   imports: [CommonModule, ReactiveFormsModule],
//   templateUrl: './stock-list.html',
//   styleUrl: './stock-list.css'
// })
// export class StockList implements OnInit {

//   stocks: any[] = [];

//   addForm!: FormGroup;
//   reduceForm!: FormGroup;

//   showAddModal = false;
//   showReduceModal = false;

//   constructor(
//     private stockService: StockService,
//     private fb: FormBuilder
//   ) {}

//   ngOnInit(): void {
//     this.initForms();
//     this.loadStocks();
//   }

//   // 🔹 FORM INITIALIZATION
//   initForms(): void {
//     this.addForm = this.fb.group({
//       productId: ['', Validators.required],
//       quantity: ['', Validators.required],
//       purchasePrice: ['', Validators.required],
//       stockDate: ['', Validators.required]
//     });

//     this.reduceForm = this.fb.group({
//       productId: ['', Validators.required],
//       quantity: ['', Validators.required]
//     });
//   }

//   // 🔹 LOAD STOCK LIST
//   loadStocks(): void {
//     this.stockService.getAll().subscribe({
//       next: (res: any) => {
//         console.log('STOCK API RESPONSE', res);
//         this.stocks = res?.data ?? [];
//       },
//       error: err => console.error('Stock load error', err)
//     });
//   }

//   // 🔹 MODAL CONTROLS
//   openAdd(): void {
//     this.addForm.reset();
//     this.showAddModal = true;
//   }

//   openReduce(): void {
//     this.reduceForm.reset();
//     this.showReduceModal = true;
//   }

//   close(): void {
//     this.showAddModal = false;
//     this.showReduceModal = false;
//   }

//   // 🔹 ADD STOCK
//   addStock(): void {
//     if (this.addForm.invalid) return;

//     const payload = {
//       productId: Number(this.addForm.get('productId')?.value),
//       quantity: Number(this.addForm.get('quantity')?.value),
//       purchasePrice: Number(this.addForm.get('purchasePrice')?.value),
//       stockDate: this.addForm.get('stockDate')?.value
//     };

//     this.stockService.addStock(payload).subscribe({
//       next: () => {
//         this.close();
//         this.loadStocks();
//       },
//       error: err => console.error('Add stock error', err)
//     });
//   }

//   // 🔹 REDUCE STOCK (FIFO)
//   reduceStock(): void {
//     if (this.reduceForm.invalid) return;

//     const productId = Number(this.reduceForm.get('productId')?.value);
//     const quantity = Number(this.reduceForm.get('quantity')?.value);

//     if (!productId || !quantity) {
//       alert('Invalid Product ID or Quantity');
//       return;
//     }

//     this.stockService.reduceFIFO(productId, quantity).subscribe({
//       next: () => {
//         this.close();
//         this.loadStocks();
//       },
//       error: err => console.error('Reduce stock error', err)
//     });
//   }

//   // 🔹 DELETE STOCK
//   delete(id: number): void {
//     if (!confirm('Delete this stock record?')) return;

//     this.stockService.delete(id).subscribe({
//       next: () => this.loadStocks(),
//       error: err => console.error('Delete stock error', err)
//     });
//   }
// }

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule
} from '@angular/forms';
import { StockService } from '../stock.service';

@Component({
  selector: 'app-stock-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './stock-list.html',
  styleUrl: './stock-list.css'
})
export class StockList implements OnInit {

  stocks: any[] = [];

  addForm!: FormGroup;
  reduceForm!: FormGroup;

  showAddModal = false;
  showReduceModal = false;

  constructor(
    private stockService: StockService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initForms();
    this.loadStocks();
  }

  // 🔹 Initialize Forms
  initForms(): void {
    this.addForm = this.fb.group({
      productId: ['', Validators.required],
      quantity: ['', Validators.required],
      purchasePrice: ['', Validators.required],
      stockDate: ['', Validators.required]
    });

    this.reduceForm = this.fb.group({
      productId: ['', Validators.required],
      quantity: ['', Validators.required]
    });
  }

  // 🔹 Load Stock Data
  loadStocks(): void {
    this.stockService.getAll().subscribe(res => {
      this.stocks = res?.data || [];
    });
  }

  // 🔹 Modal Controls
  openAdd() {
    this.addForm.reset();
    this.showAddModal = true;
  }

  openReduce() {
    this.reduceForm.reset();
    this.showReduceModal = true;
  }

  close() {
    this.showAddModal = false;
    this.showReduceModal = false;
  }

  // 🔹 Add Stock
  addStock() {
    if (this.addForm.invalid) return;

    this.stockService.addStock(this.addForm.value).subscribe(() => {
      this.close();
      this.loadStocks();
    });
  }

  // 🔹 Reduce FIFO
  reduceStock() {
    if (this.reduceForm.invalid) return;

    const { productId, quantity } = this.reduceForm.value;

    this.stockService.reduceFIFO(productId, quantity).subscribe(() => {
      this.close();
      this.loadStocks();
    });
  }

  // 🔹 Delete
  delete(id: number) {
    if (!confirm('Delete stock record?')) return;

    this.stockService.delete(id).subscribe(() => {
      this.loadStocks();
    });
  }
}
