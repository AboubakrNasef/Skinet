import { Component, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/IProduct';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from '../../basket/basket.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;
  quantity = 1;

  constructor(
    private shopService: ShopService,
    private activatedroute: ActivatedRoute,
    private basketService: BasketService
  ) {}
  ngOnInit(): void {
    this.loadProduct();
  }

  addItemToBasket() {
    this.basketService.addItemToBasket(this.product, this.quantity);
  }

  incrementQuantity() {
    this.quantity++;
  }
  decrementQuantity() {
    if  (this.quantity>1)
    {
      this.quantity--;

    }
  }
  loadProduct() {
    let id = Number(this.activatedroute.snapshot.paramMap!.get('id'));

    this.shopService.getProduct(id).subscribe({
      next: (product) => (this.product = product),
      error: (error) => console.log(error),
    });
  }
}
