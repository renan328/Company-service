import { Component } from '@angular/core';
import { Company } from 'src/app/Company';
@Component({
  selector: 'app-insert-company',
  templateUrl: './insert-company.component.html',
  styleUrls: ['./insert-company.component.css']
})
export class InsertCompanyComponent {
  btnText = "Cadastrar"

  async createHandler(company: Company) {
    console.log("deu boa") 
  }
}
