import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss'],
})
export class TestErrorComponent {
  baseUrl = environment.apiUrl;
  validationErrors:any
  constructor(private http: HttpClient) {}

  get404Error() {
    console.log(this.baseUrl + 'products/42')
    this.http.get(this.baseUrl + 'products/42').subscribe((response) => {
      console.log(response);
    },
    error=>console.log(error));
  }
  get500Error() {
    this.http.get(this.baseUrl + 'Buggy/servererror').subscribe((response) => {
      console.log(response);
    },
    error=>console.log(error));
  }
  get400Error() {
    this.http.get(this.baseUrl + 'Buggy/badrequest').subscribe((response) => {
      console.log(response);
    },
    error=>console.log(error));
  }
  get400ValidationError() {
    this.http.get(this.baseUrl + 'products/fortytwo').subscribe((response) => {
      console.log(response);
      
    },
    error=>
    {
      console.log(error);
      this.validationErrors = error.errors.id})
    }
}
