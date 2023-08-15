import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DetailsCompanyComponent } from './components/pages/details-company/details-company.component';
import { HomeComponent } from './components/pages/home/home.component';
import { InsertCompanyComponent } from './components/pages/insert-company/insert-company.component';
import { UpdateCompanyComponent } from './components/pages/update-company/update-company.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'insert', component: InsertCompanyComponent },
  { path: 'company/update/:id', component: UpdateCompanyComponent },
  { path: 'company/:id', component: DetailsCompanyComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
