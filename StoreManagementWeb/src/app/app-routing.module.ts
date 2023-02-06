import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EditStoreProductComponent } from './components/edit-store-product/edit-store-product.component';
import { StoreProductListComponent } from './components/store-product-list/store-product-list.component';
import { UploadStoreProductComponent } from './components/upload-store-product/upload-store-product.component';

const routes: Routes = [{
  path: '',
  redirectTo: 'price-list',
  pathMatch: 'full'
}, {
  path: 'price-list',
  component: StoreProductListComponent,
  data: {
    title: 'Price List'
  }
},
{
  path: 'edit/:id',
  component: EditStoreProductComponent,
  data: {
    title: 'Edit'
  }
},
{
  path: 'upload-price-feed',
  component: UploadStoreProductComponent,
  data: {
    title: 'Upload Price Feed'
  }
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
