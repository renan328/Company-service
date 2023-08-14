import { Component, OnInit } from '@angular/core';
import { Company } from 'src/app/Company';
import { CompanyService } from 'src/app/services/company.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-update-company',
  templateUrl: './update-company.component.html',
  styleUrls: ['./update-company.component.css']
})
export class UpdateCompanyComponent {
  company!: Company;
  btnText: string = 'Editar'

  constructor(private companyService: CompanyService, private route: ActivatedRoute, private router: Router) {
  }

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get("id"))

    this.companyService.getCompany(id).subscribe(item => {
      this.company = item;
    });
  }

  async updateHandler(company: Company) {
    const id = Number(this.route.snapshot.paramMap.get("id"))
    company.id = id
    
    this.companyService.updateCompany(company).subscribe()
    alert("Empresa editada com sucesso!");
    this.router.navigate(['/']);
  }
}
