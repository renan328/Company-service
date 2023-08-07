import { CompanyService } from 'src/app/services/company.service';
import { Component } from '@angular/core';
import { Company } from 'src/app/Company';

@Component({
  selector: 'app-list-companies',
  templateUrl: './list-companies.component.html',
  styleUrls: ['./list-companies.component.css']
})
export class ListCompaniesComponent {


  companies: Company[] = [
  ]

  constructor(private companyService: CompanyService) {
    this.getCompanies()
  }

  formatDocument(document: string): string {
    const formattedDocument = document.replace(/\D/g, '');
    const part1 = formattedDocument.substring(0, 2);
    const part2 = formattedDocument.substring(2, 5);
    const part3 = formattedDocument.substring(5, 8);
    const part4 = formattedDocument.substring(8, 12);
    const part5 = formattedDocument.substring(12, 14);

    return `${part1}.${part2}.${part3}/${part4}-${part5}`;
  }

  getCompanies(): void {
    this.companyService.getAll().subscribe((companies) => (this.companies = companies));
  }
}
