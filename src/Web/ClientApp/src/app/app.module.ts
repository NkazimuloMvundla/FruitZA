import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { ModalModule } from 'ngx-bootstrap/modal';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ProductComponent } from './product/product.component';
import { ProductListComponent } from './product-list/product-list.component';
import { EditProductModalComponent } from './EditProductModal/EditProductModal.component';
import { CategoryComponent } from './category/category.component';
import { CategoryListComponent } from './category-list/category-list.component';
import { EditCategoryModalComponent } from './edit-category/edit-category.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ExceptionModalComponent } from './exception-modal/exception-modal.component';
import { SuccessModalComponent } from './SuccessModal/SuccessModal.component';
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
      ProductComponent,
      ProductListComponent,
      EditProductModalComponent,
      CategoryComponent,
      CategoryListComponent,
      EditCategoryModalComponent,
      ExceptionModalComponent,
      SuccessModalComponent,
      SuccessModalComponent
   ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    PaginationModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: ProductListComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'add-product', component: ProductComponent },
      { path: 'products', component: ProductListComponent },
      { path: 'add-category', component: CategoryComponent },
      { path: 'category-list', component: CategoryListComponent }
    ]),
    BrowserAnimationsModule,
    ModalModule.forRoot()
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
