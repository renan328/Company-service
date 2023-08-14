import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http'

import { AppComponent } from './app.component';
import { ListCompaniesComponent } from './components/list-companies/list-companies.component';
import { DetailsCompanyComponent } from './components/pages/details-company/details-company.component';
import { CNPJPipe } from './cnpj.pipe';
import { HomeComponent } from './components/pages/home/home.component';
import { InsertCompanyComponent } from './components/pages/insert-company/insert-company.component';
import { FormCompanyComponent } from './components/form-company/form-company.component';
import { UpdateCompanyComponent } from './components/pages/update-company/update-company.component';
@NgModule({
  declarations: [
    AppComponent,
    ListCompaniesComponent,
    DetailsCompanyComponent,
    CNPJPipe,
    HomeComponent,
    InsertCompanyComponent,
    FormCompanyComponent,
    UpdateCompanyComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
