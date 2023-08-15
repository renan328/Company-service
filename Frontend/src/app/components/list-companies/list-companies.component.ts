import { CompanyService } from 'src/app/services/company.service';
import { Component } from '@angular/core';
import { Company } from 'src/app/Company';

@Component({
  selector: 'app-list-companies',
  templateUrl: './list-companies.component.html',
  styleUrls: ['./list-companies.component.css'],
})
export class ListCompaniesComponent {
  companies: Company[] = [];

  constructor(private companyService: CompanyService) {
    this.getCompanies();
  }

  getCompanies(): void {
    this.companyService.getAll().subscribe(
      (companies) => (this.companies = companies),
    );
  }
}
