import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListCompaniesComponent } from './components/list-companies/list-companies.component';
import { InsertCompaniesComponent } from './components/insert-companies/insert-companies.component';
import { DetailsCompanyComponent } from './components/details-company/details-company.component';

const routes: Routes = [
  {path: '', component: ListCompaniesComponent},
  {path: 'cadastro', component: InsertCompaniesComponent},
  {path: 'company/:id', component: DetailsCompanyComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
