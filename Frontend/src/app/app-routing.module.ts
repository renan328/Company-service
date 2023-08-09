import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DetailsCompanyComponent } from './components/pages/details-company/details-company.component';
import { HomeComponent } from './components/pages/home/home.component';
import { InsertCompanyComponent } from './components/pages/insert-company/insert-company.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'cadastro', component: InsertCompanyComponent},
  {path: 'company/:id', component: DetailsCompanyComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
