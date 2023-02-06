import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadStoreProductComponent } from './upload-store-product.component';

describe('UploadStoreProductComponent', () => {
  let component: UploadStoreProductComponent;
  let fixture: ComponentFixture<UploadStoreProductComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UploadStoreProductComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UploadStoreProductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
