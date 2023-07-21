import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {PaginationModule} from 'ngx-bootstrap/pagination';
import {CarouselModule} from 'ngx-bootstrap/carousel';
import { PaginHeaderComponent } from './Components/pagin-header/pagin-header.component';
import { PagerComponent } from './Components/pager/pager.component';
import { OrderTotalsComponent } from './components/order-totals/order-totals.component'


@NgModule({
  declarations: [
    PaginHeaderComponent,
    PagerComponent,
    OrderTotalsComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot()
  ]
  ,
  exports:[
    PaginationModule,
    PaginHeaderComponent,
    PagerComponent,
    CarouselModule,
    OrderTotalsComponent
  ]
})
export class SharedModule { }
