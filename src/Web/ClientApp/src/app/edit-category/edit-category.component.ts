import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { CategoryClient, CreateOrUpdateCategoryCommand, ICategoryDto } from '../web-api-client';
import { CategoryValidationService } from '../services/CategoryValidationService';
import { Subject } from 'rxjs';
import { ReloadService } from '../services/ReloadService.service';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})

export class EditCategoryModalComponent implements OnInit {
  category: ICategoryDto; // Input to receive the category data
  title?: string;
  closeBtnName?: string;
  categoryForm: FormGroup;
  submitted = false;
  reloadSubject: Subject<void> = new Subject<void>();
  constructor(private reloadService: ReloadService,public bsModalRef: BsModalRef, private fb: FormBuilder, private categoryClient: CategoryClient,  private categoryValidationService: CategoryValidationService) { }

  ngOnInit() {
    this.initForm();
  }

  initForm() {
    this.categoryForm = this.fb.group({
      categoryCode: [this.category.categoryCode, [Validators.required, this.categoryValidationService.categoryCodeFormatValidator()]],
      name: [this.category.name, Validators.required],
      id:[this.category.id, Validators.required]
    });
    console.log(this.categoryForm)
  }


  submitEditForm() {
    this.submitted = true;
    if (this.categoryForm.valid) {
      const categoryData: CreateOrUpdateCategoryCommand = this.categoryForm.value;
      console.log(categoryData)
      this.categoryClient.category_CreateCategory(categoryData).subscribe(
        () => {
          // Handle success
          this.bsModalRef.hide(); // Close modal after successful submission
          this.reloadService.emitReload(); // Emit reload signal after successful submission
        },
        (error) => {
          // Handle error
        }
      );

      console.log('Category updated successfully.');
    } else {
      console.error('Form is invalid. Please check the fields.');
    }
  }


}
