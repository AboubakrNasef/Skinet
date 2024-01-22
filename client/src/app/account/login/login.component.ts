import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { validate } from 'uuid';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  loginForm: FormGroup;
  returnUrl: string;
  constructor(private accountService: AccountService, private router: Router,private acitvatedRoute:ActivatedRoute) {}

  ngOnInit(): void {
    this.returnUrl = this.acitvatedRoute.snapshot.queryParamMap['returnUrl'] || '/shop';
    this.createLoginForm();
  }

  createLoginForm() {
    this.loginForm = new FormGroup({
      email: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }

  onSubmit() {
   console.log(this.loginForm.value.email.value)
    this.accountService.login(this.loginForm.value).subscribe(
      () => {
        this.router.navigateByUrl(this.returnUrl);
      },
      (error) => {
        console.log("onsubmit error" + error);
      }
    );
  }
}
