import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditStoreProductComponent } from './edit-store-product.component';

describe('EditStoreProductComponent', () => {
  let component: EditStoreProductComponent;
  let fixture: ComponentFixture<EditStoreProductComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditStoreProductComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditStoreProductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
