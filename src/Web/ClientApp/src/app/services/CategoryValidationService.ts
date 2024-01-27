import { Injectable } from '@angular/core';
import { AbstractControl, ValidationErrors } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class CategoryValidationService {

  constructor() { }

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
