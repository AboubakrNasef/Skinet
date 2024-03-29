import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { CoreModule } from './core/core.module';
import { ErrorInterceptor } from './core/Interceptors/error.interceptor';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './core/Interceptors/loading.interceptor';
import { HomeModule } from './home/home.module';
import { AccountRoutingModule } from './account/account-routing.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { JwtInterceptor } from './core/Interceptors/jwt.interceptor';


@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    HomeModule,
    NgxSpinnerModule.forRoot({ type: 'ball-scale-multiple' }),
    FontAwesomeModule,
   
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass:ErrorInterceptor,
      multi:true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass:LoadingInterceptor,
      multi:true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass:JwtInterceptor,
      multi:true
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
