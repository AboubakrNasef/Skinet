import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasketTotal } from '../../models/IBasket';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-order-totals',
  templateUrl: './order-totals.component.html',
  styleUrls: ['./order-totals.component.scss']
})
export class OrderTotalsComponent implements OnInit {
basketTotal$:Observable<IBasketTotal>;

constructor(private basketService:BasketService){}
  ngOnInit(): void {
   this.basketTotal$ = this.basketService.basketTotal$;
  }
}
