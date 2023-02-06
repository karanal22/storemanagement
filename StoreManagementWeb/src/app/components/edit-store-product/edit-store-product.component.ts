import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { StoreProductListModel } from '../../models/response/store-product-list.model';
import { StoreProductService } from '../../services/store-product.service';

@Component({
  selector: 'app-edit-store-product',
  templateUrl: './edit-store-product.component.html',
  styleUrls: ['./edit-store-product.component.scss']
})
export class EditStoreProductComponent implements OnInit {

  id: number = 0;
  subscription: Subscription;
  initialData: any;
  storeProductFormGroup: FormGroup;

  constructor(public _fb: FormBuilder,
    private _route: ActivatedRoute,
    private _router: Router,
    private _storeProductService: StoreProductService) {

    this.storeProductFormGroup = this._fb.group({
      storeId: [null, [Validators.required]],
      sku: [null, [Validators.required]],
      price: [null, [Validators.required]]
    });

    this.subscription = this._route.params.subscribe(x => {
      if (this._route.snapshot.params["id"] === undefined) {
        this._router.navigateByUrl('/price-list');
      }
      this.id = Number(x["id"]);
      if (this.id === NaN) {
        this._router.navigateByUrl('/price-list');
      } else {

        this.getStoreProduct();
      }
    })
  }
  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  getStoreProduct() {
    this._storeProductService.getStoreProductById(this.id).subscribe((resp: any) => {
      const data = this.initialData = resp;
      this.storeProductFormGroup.reset(data, { emitEvent: false });
    });
  }

  onSubmit() {
    if (this.storeProductFormGroup.valid) {

      const data: any = { ...this.storeProductFormGroup.value, id: this.initialData.id, date: this.initialData.date };
      data.id = this.id;
      this._storeProductService.updateStoreProduct(data).subscribe(x => {
        alert('Store product saved.');
        this._router.navigateByUrl('/price-list');
      });
    }
  }

  onCancel() {
    this._router.navigateByUrl('/price-list');
  }

  hasError(obj: string | AbstractControl) {
    let control: AbstractControl;

    if (typeof (obj) === 'string') {
      control = this.storeProductFormGroup.controls[obj];
    } else {
      control = obj;
    }
    return !!control && control.invalid && (control.dirty || control.touched);
  }

  getFormGroupControl(key: string) {
    const control = this.storeProductFormGroup.controls[key] as FormGroup;
    return control;
  }
}
