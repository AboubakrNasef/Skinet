import { CdkStepper } from '@angular/cdk/stepper';
import { ToastrService } from 'ngx-toastr';

import { Component, OnInit, Input } from '@angular/core';
import { CheckoutService } from '../checkout.service';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/IBasket';
import { FormGroup } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html',
  styleUrls: ['./checkout-review.component.scss'],
})
export class CheckoutReviewComponent implements OnInit {
  @Input() appStepper: CdkStepper;
  @Input() checkoutform: FormGroup;
  constructor(
    private checkoutService: CheckoutService,
    private basketService: BasketService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  async submitOrder() {
    const basket = this.basketService.getCurrentBasketValue();
    try {
      const createdOrder = await this.createOrder(basket);
console.log('createdOrder :>> ', createdOrder);
      if (createdOrder) {
        this.basketService.deleteBasket(basket);
        const navigationExtras: NavigationExtras = { state: createdOrder };
        this.router.navigate(['orders'], navigationExtras);
      } else {
        this.toastr.error("somethingwrong");
      }
 
    } catch (error) {
      console.log(error);

    }
  }
  private async createOrder(basket: IBasket) {
    const orderToCreate = this.getOrderToCreate(basket);
    return this.checkoutService.createOrder(orderToCreate).toPromise();
  }
  getOrderToCreate(basket: IBasket) {
    return {
      basketId: basket.id,
      deliveryMethodId: +this.checkoutform
        .get('deliveryForm')!
        .get('deliveryMethod')!.value,
      shipToAddress: this.checkoutform.get('addressForm')!.value,
    };
  }
}
