import { Component, OnInit } from '@angular/core';
import { IPaginatedListOfProductPaginationDto, IProductPaginationDto, PaginatedListOfProductPaginationDto, ProductClient, ProductPaginationDto } from '../web-api-client';

import { EditProductModalComponent } from '../EditProductModal/EditProductModal.component';
import { BsModalService, BsModalRef, ModalOptions } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  paginatedList: IPaginatedListOfProductPaginationDto;
  selectedProduct: IProductPaginationDto | undefined;
  bsModalRef?: BsModalRef;
  constructor(private productService: ProductClient,private modalService: BsModalService) { }

  ngOnInit(): void {
    //this.getProducts();
    this.loadProducts();
  }

  // getProducts(): void {
  //   this.productService.product_GetProducts(1,10)
  //     .subscribe(products => this.products = products);
  // }

  loadProducts(): void {
    // Call your service method to get the paginated list of products
    this.productService.product_GetProducts(1,10).subscribe(
      (data: IPaginatedListOfProductPaginationDto) => {
        console.log(data)
        this.paginatedList = data;
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

  //  openEditModal(product: IProductPaginationDto) {
  //   // Set the selected product to be edited
  //   this.selectedProduct = { ...product }; // Create a copy to avoid directly modifying the original product
  //   // Open the modal
  //  }

  // openEditModal(product: IProductPaginationDto) {
  //   const modalRef: BsModalRef = this.modalService.show(EditProductModalComponent);
  //   modalRef.content.product = product;
  // }

  // openModalWithComponent(product: IProductPaginationDto) {
  //   const initialState: ModalOptions = {
  //     initialState: {
  //       list: ['Open a modal with component', 'Pass your data', 'Do something else', '...'],
  //       title: 'Modal with component'
  //     }
  //   };
  //   this.bsModalRef = this.modalService.show(EditProductModalComponent, initialState);
  //   this.bsModalRef.content.closeBtnName = 'Close';
  // }

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

}
