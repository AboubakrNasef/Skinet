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
  shopParams :  ShopParams;
  totalCount = 0;
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price:Low to High', value: 'priceAsc' },
    { name: 'Price:High to Low', value: 'priceDesc' },
  ];
  @ViewChild('search', { static: true }) searchInput: ElementRef;




  constructor(private shopService: ShopService) {
    this.shopParams = shopService.getShopParams()
  }
  ngOnInit(): void {
    this.getBrands();
    this.getTypes();
    this.getProducts(true);
  }
  getProducts(useCache=false) {
    this.shopService
      .getProducts(useCache)

      .subscribe({
        next: (response) => {
          let r = response as IPagination;
          this.products = r.data;
         
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
    const params = this.shopService.getShopParams();
    params.brandId = brandId;
    params.pageNumber=1;
    this.shopService.setShopParams(params);
    this.getProducts();
  }
  onTypeSelected(typeId: number) {
    const params = this.shopService.getShopParams();
    params.typeId = typeId;
    params.pageNumber=1;
    this.shopService.setShopParams(params);
    this.getProducts();
  }

  public onSortSelected(event: Event) {
    const params = this.shopService.getShopParams();
    const sortValue = (event.target as HTMLInputElement).value;
    params.sort = sortValue;
    this.shopService.setShopParams(params);
    this.getProducts();
  }

  public onPageChanged(event: any) {
    const params = this.shopService.getShopParams();
    if(params.pageNumber!==event)
    {
      params.pageNumber = event;
      this.shopService.setShopParams(params);
      this.getProducts(true);
    }
    
  }

  public onSearch() {
    const params = this.shopService.getShopParams();
    params.search = this.searchInput.nativeElement.value;
    params.pageNumber=1;
    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onReset(){
    this.searchInput.nativeElement.value='';
    this.shopParams=new ShopParams();
    this.shopService.setShopParams( this.shopParams);
    this.getProducts();
  }
}
