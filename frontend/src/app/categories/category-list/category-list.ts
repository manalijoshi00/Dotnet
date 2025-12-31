import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, Validators, ReactiveFormsModule, FormGroup } from '@angular/forms';
import { CategoryService } from '../category.service';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './category-list.html',
  styleUrl: './category-list.css',
})
export class CategoryList implements OnInit {

  categories: any[] = [];

  categoryForm!: FormGroup;
  showModal = false;
  isEdit = false;
  selectedId!: number;

  constructor(
    private categoryService: CategoryService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadCategories();
  }

  initForm() {
    this.categoryForm = this.fb.group({
      name: ['', Validators.required],
      description: ['']
    });
  }

  loadCategories() {
    this.categoryService.getAll().subscribe({
      next: (res: any) => {
        this.categories = res?.data || [];
      },
      error: err => console.error(err)
    });
  }

  openAdd() {
    this.isEdit = false;
    this.categoryForm.reset();
    this.showModal = true;
  }

  openEdit(category: any) {
    this.isEdit = true;
    this.selectedId = category.categoryId;

    this.categoryForm.patchValue({
      name: category.name,
      description: category.description
    });
    this.showModal = true;
  }

  closeModal() {
    this.showModal = false;
  }

  submit() {
    if (this.categoryForm.invalid) return;

    const request = this.isEdit
      ? this.categoryService.update(this.selectedId, this.categoryForm.value)
      : this.categoryService.add(this.categoryForm.value);

    request.subscribe(() => {
      this.closeModal();
      this.loadCategories();
    });
  }

  delete(id: number) {
    if (confirm('Are you sure you want to delete this category?')) {
      this.categoryService.delete(id).subscribe(() => {
        this.loadCategories();
      });
    }
  }
}
