import { Component, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/IProduct';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
product:IProduct;



constructor(private shopService:ShopService
  ,private activatedroute:ActivatedRoute) {
  

}
  ngOnInit(): void {
   this.loadProduct();
  }

loadProduct(){
 


  let id  = Number(this.activatedroute.snapshot.paramMap!.get('id') )

  this.shopService.getProduct(id).subscribe({
    next:(product)=>this.product=product,
    error:(error) =>console.log(error)
  })

}
}
