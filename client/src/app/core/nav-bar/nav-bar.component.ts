import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasket } from 'src/app/shared/models/IBasket';
import { BasketService } from '../../basket/basket.service';
import { AccountService } from 'src/app/account/account.service';
import { IUser } from 'src/app/shared/models/IUser';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
  basket$: Observable<IBasket>;
  currentUser$: Observable<IUser>;
  constructor(
    private BasketService: BasketService,
    private accountService: AccountService
  ) {}
  ngOnInit(): void {
    this.basket$ = this.BasketService.basket$;
    this.currentUser$ = this.accountService.currentUser$;
    this.currentUser$.subscribe((user) => console.log('From navbar' + user));
  }

  logout(){this.accountService.logout()}
}
