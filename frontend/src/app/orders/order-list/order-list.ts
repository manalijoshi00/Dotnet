import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { OrderService } from '../order.service';

@Component({
  selector: 'app-order-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './order-list.html',
  styleUrl: './order-list.css'
})
export class OrderList implements OnInit {

  orders: any[] = [];
  showCreateModal = false;

  orderForm!: FormGroup;

  constructor(
    private orderService: OrderService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadOrders();
  }

  initForm(): void {
    this.orderForm = this.fb.group({
      productId: ['', Validators.required],
      quantity: ['', [Validators.required, Validators.min(1)]]
    });
  }

  loadOrders(): void {
    this.orderService.getAll().subscribe({
      next: (res: any) => {
        console.log('ORDER RESPONSE', res);
        this.orders = res?.data ?? [];
      },
      error: err => console.error(err)
    });
  }

  openCreate(): void {
    this.orderForm.reset();
    this.showCreateModal = true;
  }

  close(): void {
    this.showCreateModal = false;
  }

  createOrder(): void {
    if (this.orderForm.invalid) return;

    const payload = {
      items: [
        {
          productId: Number(this.orderForm.get('productId')?.value),
          quantity: Number(this.orderForm.get('quantity')?.value)
        }
      ]
    };

    console.log('ORDER PAYLOAD', payload);

    this.orderService.create(payload).subscribe({
      next: () => {
        this.close();
        this.loadOrders();
      },
      error: err => {
        alert(err?.error?.message || 'Order failed');
        console.error(err);
      }
    });
  }
}