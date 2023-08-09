import { Component } from '@angular/core';
import { Company, CompanyAddress, CompanyTelephone } from 'src/app/Company';

@Component({
  selector: 'app-form-company',
  templateUrl: './form-company.component.html',
  styleUrls: ['./form-company.component.css']
})
export class FormCompanyComponent {
  company: Company = {
    id: 0,
    name: '',
    document: '',
    createDate: '',
    updateDate: '',
    companyAddresses: []
  };

  constructor() { }

  addPhone(addressIndex: number): void {
    this.company.companyAddresses[addressIndex].companyTelephones.push({
      id: 0,
      companyAddress: 0,
      phoneNumber: '',
      createDate: '',
      updateDate: ''
    });
  }


  removePhone(addressIndex: number, phoneIndex: number): void {
    if (this.company.companyAddresses[addressIndex].companyTelephones.length > 1) {
      this.company.companyAddresses[addressIndex].companyTelephones.splice(phoneIndex, 1);
    }
  }

  addAddress(): void {
    const newAddress: CompanyAddress = {
      id: this.company.companyAddresses.length + 1,
      companyId: this.company.id,
      street: '',
      neighborhood: '',
      city: '',
      postalCode: '',
      country: '',
      createDate: '',
      updateDate: '',
      companyTelephones: []
    };
    this.company.companyAddresses.push(newAddress);
  }

  removeAddress(index: number): void {
    this.company.companyAddresses.splice(index, 1);
  }
}
