import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasket } from 'src/app/shared/models/Basket';
import { BasketService } from '../../basket/basket.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
  basket$: Observable<IBasket>;
  constructor(private BasketService: BasketService) {}
  ngOnInit(): void {
    this.basket$ = this.BasketService.basket$;
  }
}
