import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Company } from 'src/app/Company';
import { CompanyService } from 'src/app/services/company.service';

@Component({
  selector: 'app-details-company',
  templateUrl: './details-company.component.html',
  styleUrls: ['./details-company.component.css']
})
export class DetailsCompanyComponent {
  company?: Company

  constructor(private companyService: CompanyService, private route: ActivatedRoute) {
    this.getCompany();
  }

  // removeCompany(company: Company) {
  //   this.company = this.company?.filter((a) => this.company?.name != a.name)
  // }

  getCompany() {
    const id = Number(this.route.snapshot.paramMap.get("id"))
    this.companyService.getCompany(id).subscribe((company) => (this.company = company))
  }
}
