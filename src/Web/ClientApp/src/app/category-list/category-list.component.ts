import { Component, OnInit } from '@angular/core';
import { CategoryClient, ICategoryDto, IPaginatedListOfCategoryDto, IPaginatedListOfProductPaginationDto, IProductPaginationDto } from '../web-api-client';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { EditCategoryModalComponent } from '../edit-category/edit-category.component';
import { Router } from '@angular/router';
import { ReloadService } from '../services/ReloadService.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit {
  paginatedList: IPaginatedListOfCategoryDto;
  selectedProduct: ICategoryDto | undefined;
  bsModalRef?: BsModalRef;
  reloadSubscription: Subscription;
  searchQuery: string = '';
  currentPage: number = 1;
  pageSize: number = 10; // Adjust as needed
  totalItems: number = 0;
  maxSize: number = 5; // Adjust as needed
  constructor(private reloadService: ReloadService,private categoryService: CategoryClient,private modalService: BsModalService,private router: Router) { }

  ngOnInit() {
    this.loadCategories();
    this.reloadSubscription = this.reloadService.getReloadSubject().subscribe(() => {
      // Perform actions to reload data or refresh view
      this.loadCategories(1);
    });
  }

  loadCategories(pageNumber: number = 1): void {
    // Call your service method to get the paginated list of products
    this.categoryService.category_GetCategories(1,10).subscribe(
      (data: IPaginatedListOfCategoryDto) => {
        console.log(data)
        this.paginatedList = data;
      },
      error => {
        console.log('Error fetching products:', error);
      }
    );
  }

  pageChanged(event: any): void {
    this.loadCategories(event.page);
  }

  openModalWithComponent(category: any) {
    const initialState: ModalOptions = {
      initialState: {
        category: category,
        title: 'Edit Category'
      }
    };
    this.bsModalRef = this.modalService.show(EditCategoryModalComponent, initialState);
    this.bsModalRef.content.closeBtnName = 'Close';
  }

  navigateToAddCategory() {
    this.router.navigate(['/add-category']); // Navigate to the Add Category page
  }

  ngOnDestroy() {
    this.reloadSubscription.unsubscribe();
  }

}
