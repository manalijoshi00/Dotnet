import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, Validators, ReactiveFormsModule, FormGroup } from '@angular/forms';
import { SupplierService } from '../supplier.service';

@Component({
  selector: 'app-supplier-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './supplier-list.html',
  styleUrl: './supplier-list.css',
})
export class SupplierList implements OnInit {

  suppliers: any[] = [];

  supplierForm!: FormGroup;
  showModal = false;
  isEdit = false;
  selectedId!: number;

  constructor(
    private supplierService: SupplierService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadSuppliers();
  }

  initForm() {
    this.supplierForm = this.fb.group({
      name: ['', Validators.required],
      contactPerson: [''],
      phone: [''],
      email: ['']
    });
  }

  loadSuppliers() {
    this.supplierService.getAll().subscribe({
      next: (res: any) => {
        this.suppliers = res?.data || [];
      },
      error: err => console.error(err)
    });
  }

  openAdd() {
    this.isEdit = false;
    this.supplierForm.reset();
    this.showModal = true;
  }

  openEdit(supplier: any) {
    this.isEdit = true;
    this.selectedId = supplier.supplierId;
    this.supplierForm.patchValue(supplier);
    this.showModal = true;
  }

  closeModal() {
    this.showModal = false;
  }

  submit() {
    if (this.supplierForm.invalid) return;

    const request = this.isEdit
      ? this.supplierService.update(this.selectedId, this.supplierForm.value)
      : this.supplierService.add(this.supplierForm.value);

    request.subscribe(() => {
      this.closeModal();
      this.loadSuppliers();
    });
  }

  delete(id: number) {
    if (confirm('Delete supplier?')) {
      this.supplierService.delete(id).subscribe(() => {
        this.loadSuppliers();
      });
    }
  }
}
