import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { StoreProductListModel } from '../../models/response/store-product-list.model';
import { StoreProductService } from '../../services/store-product.service';

@Component({
  selector: 'app-store-product-list',
  templateUrl: './store-product-list.component.html',
  styleUrls: ['./store-product-list.component.scss']
})
export class StoreProductListComponent implements OnInit {

  public data: StoreProductListModel[] = [];
  public paginationOption = {
    itemsPerPage: 10,
    maxSize: 5,
    currentPage: 1,
    totalItems: 0,
    pageSizeList: [10, 20, 50]
  };

  public serachModel = {
    storeId: null,
    productName: "",
    sku: "",
  };

  productListSearchFormGroup: FormGroup;

  constructor(private _fb: FormBuilder,
    private _storeProductService: StoreProductService) {
    this.productListSearchFormGroup = this._fb.group({
      storeId: [null],
      productName: [null],
      sku: [null],
    });
  }

  ngOnInit(): void {
    this.search();
  }

  private productListSearchFormGroupSetUpForm() {
    this.productListSearchFormGroup = this._fb.group({
      storeId: [null],
      donorName: [null],
      sku: [null],
    });
  }

  public updateData(event?: any) {
    this._storeProductService.getStoreProductList({
      pageNumber: event?.page ?? 1,
      pageSize: event?.itemsPerPage ?? this.paginationOption.itemsPerPage,
      sortBy: [{ "date": "desc" }],
      ...this.serachModel
    }).subscribe((resp: any) => {
      const data = resp;
      this.data = data.records;
      this.paginationOption.totalItems = data.totalRecords;
    });
  }

  public search() {
    if (this.productListSearchFormGroup.valid) {
      const data: any = { ...this.productListSearchFormGroup.value }
      this.serachModel = data;
      // If current page is 1 then we need to manual fire update event
      // else changing current page property will fire event
      if (this.paginationOption.currentPage !== 1) {
        this.paginationOption.currentPage = 1;
      } else {
        this.updateData();
      }
    }
  }

  public resetSearch() {
    this.productListSearchFormGroupSetUpForm();
    this.serachModel = {
      storeId: null,
      productName: "",
      sku: "",
    };
    if (this.paginationOption.currentPage !== 1) {
      this.paginationOption.currentPage = 1;
    } else {
      this.updateData();
    }
  }

}
