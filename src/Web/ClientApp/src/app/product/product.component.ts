import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductClient, FileParameter, CategoryClient, IPaginatedListOfCategoryDto, ICategoryDto, FileResponse, ExcelFileDto, PaginatedListOfExcelFileDto, IExcelFileDto } from '../web-api-client';
import { Router } from '@angular/router';
import { ReloadService } from '../services/ReloadService.service';
import { saveAs } from 'file-saver';
import { ExceptionModalService } from '../services/exception-modal.service';
import { SuccessModalService } from '../success-modal-service/success-modal-service.component';


@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  productForm: FormGroup;
  excelForm: FormGroup;
  fileToUpload: File | null = null;
  submitted = false;
  categories: ICategoryDto[] = []; // Store the list of categories
  selectedImage: string | ArrayBuffer | null = null;
  uploadedExcelFiles: PaginatedListOfExcelFileDto;
  currentPage: number = 1;
  pageSize: number = 10; // Adjust as needed
  totalItems: number = 0;
  maxSize: number = 5; // Adjust as needed
  constructor(private successModalService: SuccessModalService,private exceptionModalService: ExceptionModalService, private router: Router,private reloadService: ReloadService, private fb: FormBuilder, private categoryService: CategoryClient, private productService: ProductClient) { }

  ngOnInit() {
    this.initForm();
    this.initExcelForm();
    this.getCategories();
    this.getUploadedExcelFiles(1);
  }

  initForm() {
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      categoryName: ['', Validators.required],
      price: ['', [Validators.required, Validators.min(0)]],
      image: ['']
    });
  }

  initExcelForm() {
    this.excelForm = this.fb.group({
      excelFile: [null, Validators.required]
    });
  }

  pageChanged(event: any): void {
    this.getUploadedExcelFiles(event.page);
  }


  getUploadedExcelFiles(pageNumber: number = 1): void {
    this.productService.product_GetUploadedExcelFiles(1, 10)
    .subscribe(
      files => {
        console.log('Uploaded Excel Files:', files); // Add console.log statement here
        this.uploadedExcelFiles = files;
      },
      error => {
        console.error('Error fetching uploaded Excel files:', error); // Handle error
      }
    );

  }

  handleFileInput(event: any) {
    const files = event.target.files;

    if (files && files.length > 0) {
      this.fileToUpload = files.item(0);
    }
  }

  downloadFile(fileId: number): void {
    this.productService.product_DownloadUploadedExcel(fileId).subscribe(
      (fileResponse: FileResponse) => {
        const blob = new Blob([fileResponse.data], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
        const fileName = `file_${fileId}.xlsx`; // Adjust the file name as needed
        saveAs(blob, fileName);
      },
      (error) => {
        console.error('Error downloading file:', error);
        // Handle error
        this.exceptionModalService.openModal(error);
      }
    );
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

  getBase64Image(base64String: string): string {
    return 'data:image/png;base64,' + base64String;
  }

  onExcelUpload(): void {
    let fileParameter: FileParameter | null = null; // Declare the variable outside the if block
    // const productData = new FormData();
    if (this.fileToUpload){
      fileParameter = {
        data: this.fileToUpload,
        fileName: this.fileToUpload.name
      };
      this.productService.product_UpdloadExcel(fileParameter).subscribe(
        (response: FileResponse) => {
          // Handle successful upload response
          console.log('Excel file uploaded successfully:', response);
          this.successModalService.openModal('Excel file uploaded successfully');
        },
        (error: any) => {
          // Handle error
          console.error('Error uploading Excel file:', error);
          this.exceptionModalService.openModal(error);
        }
      );
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
        this.exceptionModalService.openModal(error);
      }
    );
  }

  onSubmit() {
    this.submitted = true;
    if (this.productForm.valid) {
      let fileParameter: FileParameter | null = null; // Declare the variable outside the if block
      // const productData = new FormData();
      if (this.fileToUpload){
        fileParameter = {
          data: this.fileToUpload,
          fileName: this.fileToUpload.name
        };

      }

      this.productService.product_CreateProduct(
        this.productForm.value.productCode,
        this.productForm.value.name,
        this.productForm.value.description,
        this.productForm.value.categoryName,
        this.productForm.value.price,
        fileParameter ?? null
        ).subscribe(
        (productId) => {
          // Handle success, e.g., show a success message
          console.log('Product created successfully. Product ID:', productId);
          this.successModalService.openModal('Product created successfully');
          this.productForm.reset();
          this.reloadService.emitReload(); // Emit reload signal after successful submission
          this.router.navigate(['/products']); // Navigate to the Add Category page
        },
        (error) => {
          // Handle error, e.g., show an error message
          console.error('Error creating product:', error);
         // this.exceptionModalService.openModal(error);
        }
      );
    } else {
      // Form is invalid, show an error message or handle accordingly
      console.error('Form is invalid. Please check the fields.');
    }
  }
}
