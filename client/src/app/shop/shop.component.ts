import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProduct } from '../shared/models/IProduct';
import { ShopService } from './shop.service';
import { IBrand } from '../shared/models/IBrand';
import { IProductType } from '../shared/models/IProductType';
import { ShopParams } from '../shared/models/ShopParams';
import { filter } from 'rxjs/operators';
import { skipNull } from '../shared/Helpers/FilterEmptyFunc';
import { IPagination } from '../shared/models/Pagination';
import { ValidationErrors } from '@angular/forms';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  products?: IProduct[];
  brands: IBrand[];
  types: IProductType[];
  shopParams = new ShopParams();
  totalCount = 0;
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price:Low to High', value: 'priceAsc' },
    { name: 'Price:High to Low', value: 'priceDesc' },
  ];
  @ViewChild('search', { static: true }) searchInput: ElementRef;




  constructor(private shopService: ShopService) {}
  ngOnInit(): void {
    this.getBrands();
    this.getTypes();
    this.getProducts();
  }
  getProducts() {
    this.shopService
      .getProducts(this.shopParams)

      .subscribe({
        next: (response) => {
          let r = response as IPagination;
          this.products = r.data;
          this.shopParams.pageNumber = r.pageIndex;
          this.totalCount = r.count;
        },
        error: (error) => console.log(error),
      });
  }
  getBrands() {
    this.shopService.getBrands().subscribe(
      (response) => (this.brands = [{ id: 0, name: 'All' }, ...response]),
      (error) => console.log(error)
    );
  }
  getTypes() {
    this.shopService.getTypes().subscribe(
      (response) => (this.types = [{ id: 0, name: 'All' }, ...response]),
      (error) => console.log(error)
    );
  }

  onBrandSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber=1;
    this.getProducts();
  }
  onTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber=1;
    this.getProducts();
  }

  public onSortSelected(event: Event) {

    const sortValue = (event.target as HTMLInputElement).value;
    this.shopParams.sort = sortValue;
   
    this.getProducts();
  }

  public onPageChanged(event: any) {
    if(this.shopParams.pageNumber!==event)
    {
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
    
  }

  public onSearch() {
    this.shopParams.search = this.searchInput.nativeElement.value;
    this.shopParams.pageNumber=1;
    this.getProducts();
  }

  onReset(){
    this.searchInput.nativeElement.value=undefined;
    this.shopParams=new ShopParams();
    this.getProducts();
  }
}
