import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import {HttpClientModule} from '@angular/common/http' 

import { AppComponent } from './app.component';
import { ListCompaniesComponent } from './components/list-companies/list-companies.component';
import { InsertCompaniesComponent } from './components/insert-companies/insert-companies.component';

@NgModule({
  declarations: [
    AppComponent,
    ListCompaniesComponent,
    InsertCompaniesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
