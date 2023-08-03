import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IOrder } from '../shared/models/Order';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getOrdersForUser(){
    return this.http.get<IOrder[]>(this.baseUrl + 'order');
  }

  getOrderDetailed(id: number){
    return this.http.get(this.baseUrl + 'order/' + id);
  }
}
