import { Component, OnInit } from '@angular/core';
import { IPaginatedListOfProductPaginationDto, IProductPaginationDto, PaginatedListOfProductPaginationDto, ProductClient, ProductPaginationDto } from '../web-api-client';

import { EditProductModalComponent } from '../EditProductModal/EditProductModal.component';
import { BsModalService, BsModalRef, ModalOptions } from 'ngx-bootstrap/modal';
import { ReloadService } from '../services/ReloadService.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { ExceptionModalService } from '../services/exception-modal.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  paginatedList: IPaginatedListOfProductPaginationDto;
  selectedProduct: IProductPaginationDto | undefined;
  bsModalRef?: BsModalRef;
  reloadSubscription: Subscription;
  searchQuery: string = '';
  currentPage: number = 1;
  pageSize: number = 10; // Adjust as needed
  totalItems: number = 0;
  maxSize: number = 5; // Adjust as needed
  constructor( private exceptionModalService: ExceptionModalService, private router: Router,private reloadService: ReloadService,private productService: ProductClient,private modalService: BsModalService) { }

  ngOnInit(): void {
    //this.getProducts();
    this.loadProducts(1);
    this.reloadSubscription = this.reloadService.getReloadSubject().subscribe(() => {
      this.loadProducts(1);
    });
  }

   // Use this computed property for filtering products based on the search term
   get filteredProducts(): any[] {
    return this.paginatedList?.items.filter(product =>
      product.name.toLowerCase().includes(this.searchQuery.toLowerCase())
      // Add other properties for search as needed
    );
  }

  pageChanged(event: any): void {
    this.loadProducts(event.page);
  }

  loadProducts(pageNumber: number = 1) {
    // Call your service method to get the paginated list of products
    this.productService.product_GetProducts(pageNumber, this.pageSize).subscribe(
      (data: IPaginatedListOfProductPaginationDto) => {
        console.log(data);
        this.paginatedList = data;
        this.totalItems = data.totalCount;
      },
      error => {
        console.log('Error fetching products:', error);
      }
    );
  }

   // Method to return the base64 image directly
   getBase64Image(base64String: string): string {
    return 'data:image/png;base64,' + base64String;
   }


  openModalWithComponent(product: IProductPaginationDto) {
    const initialState: ModalOptions = {
      initialState: {
        product: product,
        title: 'Edit Product'
      }
    };
    this.bsModalRef = this.modalService.show(EditProductModalComponent, initialState);
    this.bsModalRef.content.closeBtnName = 'Close';
  }

  deleteProduct(productId: number) {
    if (confirm('Are you sure you want to delete this product?')) {
      this.productService.product_DeleteProduct(productId).subscribe(
        () => {
          // Reload products after deletion
          this.loadProducts(1);
          this.bsModalRef.hide(); // Close modal after successful submission
          this.reloadService.emitReload(); // Emit reload signal after successful submission
          this.router.navigate(['/products']); // Navigate to the Add Category page
        },
        error => {
          console.error('Error deleting product:', error);
          this.exceptionModalService.openModal(error);
        }
      );
    }


  }

  ngOnDestroy() {
    // Unsubscribe to avoid memory leaks
    this.reloadSubscription.unsubscribe();
  }

}
