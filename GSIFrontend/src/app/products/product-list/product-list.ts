import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './product-list.html',
  styleUrl: './product-list.css'
})
export class ProductList implements OnInit {

  products: any[] = [];

  productForm!: FormGroup;
  showModal = false;
  isEdit = false;
  selectedId!: number;


  constructor(
    private productService: ProductService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.initForm();
    this.loadProducts();
  }

  initForm() {
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      barcode: [''],
      price: ['', Validators.required],
      categoryId: ['', Validators.required],
      supplierId: ['', Validators.required]
    });
  }

  loadProducts() {
    this.productService.getAll().subscribe({
      next: (res: any) => {
        this.products = res?.data || [];
      },
      error: err => console.error(err)
    });
  }

  openAdd() {
    this.isEdit = false;
    this.productForm.reset();
    this.showModal = true;
  }

  openEdit(product: any) {
    this.isEdit = true;
    this.selectedId = product.productId;
    
    this.productForm.patchValue(product);
    this.showModal = true;
  }

  closeModal() {
    this.showModal = false;
  }

  submit() {
    if (this.productForm.invalid) return;

    const request = this.isEdit
      ? this.productService.update(this.selectedId, this.productForm.value)
      : this.productService.add(this.productForm.value);

    request.subscribe(() => {
      this.closeModal();
      this.loadProducts();
    });
  }

  delete(id: number) {
    if (confirm('Delete product?')) {
      this.productService.delete(id).subscribe(() => {
        this.loadProducts();
      });
    }
  }
}