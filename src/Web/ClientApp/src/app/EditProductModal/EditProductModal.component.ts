import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CategoryClient, FileParameter, ICategoryDto, IPaginatedListOfCategoryDto, IProductPaginationDto, ProductClient } from '../web-api-client';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ReloadService } from '../services/ReloadService.service';
import { Subscription } from 'rxjs';
import { ExceptionModalService } from '../services/exception-modal.service';

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
  categories: ICategoryDto[] = []; // Store the list of categories
  submitted = false;
  reloadSubscription: Subscription;
  constructor( private exceptionModalService: ExceptionModalService,private categoryService: CategoryClient,private router: Router,private reloadService: ReloadService,public bsModalRef: BsModalRef, private fb: FormBuilder, private productsClient: ProductClient) {}

  ngOnInit() {
    this.initForm();
    this.getCategories();
    this.setInitialValues();
    this.reloadSubscription = this.reloadService.getReloadSubject().subscribe(() => {

    });
  }

  setInitialValues() {
    this.productForm.patchValue({
      categoryName: this.product.categoryName
    });
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
    this.submitted = true
    if (this.productForm.valid) {
      const productData = new FormData();
      let fileParameter: FileParameter | null = null; // Declare the variable outside the if block
      // const productData = new FormData();
      if (this.fileToUpload){
        fileParameter = {
          data: this.fileToUpload,
          fileName: this.fileToUpload.name
        };

      }
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
          this.productForm.reset();
          this.submitted = false;
          this.bsModalRef.hide(); // Close modal after successful submission
          this.reloadService.emitReload(); // Emit reload signal after successful submission
          this.router.navigate(['/products']); // Navigate to the Add Category page
        },
        (error) => {
          // Handle error, e.g., show an error message
          console.error('Error updating product:', error);
          this.exceptionModalService.openModal(error);
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

  getCategories() {
    this.categoryService.category_GetCategories(1, 10).subscribe(
      (data: IPaginatedListOfCategoryDto) => {
        console.log(data);
        this.categories = data.items; // Assuming categories are stored in 'items' property
      },
      error => {
        console.log('Error fetching categories:', error);
      }
    );
  }

  ngOnDestroy() {
    // Unsubscribe to avoid memory leaks
    this.reloadSubscription.unsubscribe();
  }

}
