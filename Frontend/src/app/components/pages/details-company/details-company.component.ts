import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Company } from 'src/app/Company';
import { CompanyService } from 'src/app/services/company.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-details-company',
  templateUrl: './details-company.component.html',
  styleUrls: ['./details-company.component.css']
})
export class DetailsCompanyComponent {
  company?: Company

  constructor(private companyService: CompanyService, private route: ActivatedRoute, private router: Router) {
    this.getCompany();
  }

  getCompany() {
    const id = Number(this.route.snapshot.paramMap.get("id"))
    this.companyService.getCompany(id).subscribe((company) => (this.company = company))
  }

  deleteCompany() {
    const id = Number(this.route.snapshot.paramMap.get("id"));

    var r = confirm("Confirmar para deletar. É uma ação irreversível!");
    if (r == true) {
      this.companyService.deleteComapny(id).subscribe();
      this.router.navigate(['/'])
    }
  }
}
