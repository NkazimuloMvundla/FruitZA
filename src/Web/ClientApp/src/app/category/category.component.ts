import { Component, OnInit } from '@angular/core';
import { CategoryClient, CreateOrUpdateCategoryCommand } from '../web-api-client';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ReloadService } from '../services/ReloadService.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  categoryForm: FormGroup;
  submitted = false;

  constructor(private router: Router,private reloadService: ReloadService,public bsModalRef: BsModalRef,private fb: FormBuilder, private categoryService: CategoryClient) { }

  ngOnInit() {
    this.initForm();
  }

  initForm() {
    this.categoryForm = this.fb.group({
      categoryCode: ['', [Validators.required, this.categoryCodeFormatValidator()]],
      name: ['', Validators.required]
    });
  }

  onSubmit() {
    this.submitted = true;
    if (this.categoryForm.valid) {
      const categoryData = this.categoryForm.value as CreateOrUpdateCategoryCommand;
      this.categoryService.category_CreateCategory(categoryData).subscribe(
        () => {
          console.log('Category created successfully.');
          // Optionally reset the form after successful submission
          this.categoryForm.reset();
          this.submitted = false;
          this.bsModalRef.hide(); // Close modal after successful submission
          this.reloadService.emitReload(); // Emit reload signal after successful submission
          this.router.navigate(['/category-list']); // Navigate to the Add Category page
        },
        (error) => {
          console.error('Error creating category:', error);
        }
      );
    } else {
      // Form is invalid, mark all fields as touched to display validation messages
      this.categoryForm.markAllAsTouched();
    }
  }

  categoryCodeFormatValidator(): any {
    return (control: AbstractControl): ValidationErrors | null => {
      const value = control.value;
      const regex = /^[A-Za-z]{3}\d{3}$/;
      if (regex.test(value)) {
        return null; // Valid
      } else {
        return { invalidCategoryCodeFormat: true }; // Invalid
      }
    };
  }
}
