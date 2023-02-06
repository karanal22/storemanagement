import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StoreProductListComponent } from './components/store-product-list/store-product-list.component';
import { UploadStoreProductComponent } from './components/upload-store-product/upload-store-product.component';
import { EditStoreProductComponent } from './components/edit-store-product/edit-store-product.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { PaginationModule } from 'ngx-bootstrap/pagination'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    StoreProductListComponent,
    UploadStoreProductComponent,
    EditStoreProductComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    PaginationModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
