import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductsClient, CreateProductCommand, ProductClient, FileParameter } from '../web-api-client';


@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  productForm: FormGroup;
  fileToUpload: File | null = null;
  categories: string[] = ['Category1', 'Category2', 'Category3']; // Replace with actual categories

  constructor(private fb: FormBuilder, private productsClient: ProductClient) { }

  ngOnInit() {
    this.initForm();
  }

  initForm() {
    this.productForm = this.fb.group({
      productCode: ['', Validators.required],
      name: ['', Validators.required],
      description: [''],
      categoryName: ['', Validators.required],
      price: ['', [Validators.required, Validators.min(0)]],
      image: ['']
    });
  }

  handleFileInput(event: any) {
    const files = event.target.files;

    if (files && files.length > 0) {
      this.fileToUpload = files.item(0);
    }
  }

  onSubmit() {
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
          console.log('Product created successfully. Product ID:', productId);
        },
        (error) => {
          // Handle error, e.g., show an error message
          console.error('Error creating product:', error);
        }
      );
    } else {
      // Form is invalid, show an error message or handle accordingly
      console.error('Form is invalid. Please check the fields.');
    }
  }
}
