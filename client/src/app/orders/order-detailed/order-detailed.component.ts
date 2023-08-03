import { OrdersService } from './../orders.service';

import { ActivatedRoute } from '@angular/router';

import { Component, OnInit } from '@angular/core';
import { IOrder } from 'src/app/shared/models/Order';

@Component({
  selector: 'app-order-detailed',
  templateUrl: './order-detailed.component.html',
  styleUrls: ['./order-detailed.component.scss']
})
export class OrderDetailedComponent implements OnInit {
  order: IOrder;

  constructor(
    private route: ActivatedRoute,

    private ordersService: OrdersService
  ) {
    
  }

  ngOnInit(): void {
    this.ordersService.getOrderDetailed(+this.route.snapshot.paramMap.get('id')!)
      .subscribe((order: any) => {
      this.order = order;
     
    }, error => {
      console.log(error);
    });
  }

}
