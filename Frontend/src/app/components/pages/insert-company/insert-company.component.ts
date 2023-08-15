import { Component } from '@angular/core';
import { Company } from 'src/app/Company';
import { CompanyService } from 'src/app/services/company.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-insert-company',
  templateUrl: './insert-company.component.html',
  styleUrls: ['./insert-company.component.css'],
})
export class InsertCompanyComponent {
  btnText: string = 'Cadastrar';

  constructor(private companyService: CompanyService, private router: Router) {}

  async createHandler(company: Company) {
    this.companyService.insertCompany(company).subscribe((response: any) => {
      alert('Empresa cadastrada com sucesso!');
      this.router.navigate(['/']);
    });
  }
}
