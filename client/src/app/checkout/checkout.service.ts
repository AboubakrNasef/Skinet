
import { map } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { IDeliveryMethod } from '../shared/models/IDeliveryMethod';
import { IOrderToCreate } from '../shared/models/Order';


@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  createOrder(order: IOrderToCreate){
    console.log('object :>> ', order);
    return this.http.post(this.baseUrl + 'Order', order);
  }

  getDeliveryMethods(){
    return this.http.get<IDeliveryMethod[]>(this.baseUrl + 'Order/deliveryMethods').pipe(
      map((dm: IDeliveryMethod[]) => {
        
        return dm.sort((a, b) => b.price - a.price);
      })
    );
  }
}
