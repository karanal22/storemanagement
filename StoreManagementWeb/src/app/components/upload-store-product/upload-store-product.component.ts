import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { resolveAny } from 'dns';
import { StoreProductService } from '../../services/store-product.service';

@Component({
  selector: 'app-upload-store-product',
  templateUrl: './upload-store-product.component.html',
  styleUrls: ['./upload-store-product.component.scss']
})
export class UploadStoreProductComponent implements OnInit {

  constructor(private _storeProductService: StoreProductService,
    private _router: Router  ) { }

  ngOnInit(): void {
  }

  public handelChange(event: any) {
    const file = event.target.files[0];
    this._storeProductService.UploadPriceFeed(file).subscribe((resp: any) => {
      alert("File Uploaded");
      this._router.navigateByUrl('/price-list');
    }, (err: any) => {
      console.log(err);
    });

  }
}
