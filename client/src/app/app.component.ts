import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IProduct } from './shared/models/IProduct';
import { IPagination } from './shared/models/Pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent  implements OnInit {
 
  title = 'SkiNet';



 constructor() {
  

 }
 
  ngOnInit(): void {
 

}
}
