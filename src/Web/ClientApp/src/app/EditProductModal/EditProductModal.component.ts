import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FileParameter, IProductPaginationDto, ProductClient } from '../web-api-client';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-EditProductModal',
  templateUrl: './EditProductModal.component.html',
  styleUrls: ['./EditProductModal.component.css']
})
export class EditProductModalComponent implements OnInit {
  // @Input() product: IProductPaginationDto; // Input to receive the product data
  // @Output() saveChangesEvent = new EventEmitter<IProductPaginationDto>(); // Output event to emit edited product data
  title?: string;
  closeBtnName?: string;
  list: string[] = [];
  product:IProductPaginationDto;
  selectedImage: string | ArrayBuffer | null = null;
  productForm: FormGroup;
  fileToUpload: File | null = null;
  constructor(public bsModalRef: BsModalRef, private fb: FormBuilder, private productsClient: ProductClient) {}

  ngOnInit() {
    this.initForm();
  }

  initForm() {
    this.productForm = this.fb.group({
      productCode: [this.product.productCode, Validators.required], // Assuming productCode is readonly
      name: [this.product.name, Validators.required],
      description: [this.product.description],
      categoryName: [this.product.categoryName, Validators.required],
      price: [this.product.price, [Validators.required, Validators.min(0)]],
      image: [this.product.image]
    });
  }

  handleFileInput(event: any) {
    const files = event.target.files;

    if (files && files.length > 0) {
      this.fileToUpload = files.item(0);
    }
  }

  submitEditForm() {
    if (this.productForm.valid) {
      const productData = new FormData();
      const fileParameter: FileParameter = {
        data: this.fileToUpload,
        fileName: this.fileToUpload.name
      };

      this.productsClient.product_CreateProduct(
        this.productForm.value.productCode,
        this.productForm.value.name,
        this.productForm.value.description,
        this.productForm.value.categoryName,
        this.productForm.value.price,
        fileParameter
        ).subscribe(
        (productId) => {
          // Handle success, e.g., show a success message
          console.log('Product updated successfully. Product ID:', productId);
        },
        (error) => {
          // Handle error, e.g., show an error message
          console.error('Error updating product:', error);
        }
      );
    } else {
      // Form is invalid, show an error message or handle accordingly
      console.error('Form is invalid. Please check the fields.');
    }
  }


  closeModal() {
    // Implement logic to close the modal
  }

  saveChanges() {
    // Emit event with edited product data
   // this.saveChangesEvent.emit(this.product);
    // Implement logic to save changes
  }

  getBase64Image(base64String: string): string {
    return 'data:image/png;base64,' + base64String;
  }


  onFileSelected(event: any): void {
    const file: File = event.target.files[0];
    if (file) {
      this.fileToUpload = file; // Update fileToUpload
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.selectedImage = reader.result;
      };
    }
  }

}
