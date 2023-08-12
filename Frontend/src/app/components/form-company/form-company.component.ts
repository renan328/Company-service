import { Component, EventEmitter, Output } from '@angular/core';
import { Company } from 'src/app/Company';
import { CompanyAddress } from 'src/app/CompanyAddress';
import { FormBuilder, FormControl, FormGroup, FormArray, AbstractControl } from '@angular/forms';

@Component({
  selector: 'app-form-company',
  templateUrl: './form-company.component.html',
  styleUrls: ['./form-company.component.css']
})
export class FormCompanyComponent {
  @Output() onSubmit = new EventEmitter<Company>();
  company: Company = {
    name: '',
    document: '',
    companyAddresses: []
  };

  companyForm: FormGroup;

  constructor(private formBuilder: FormBuilder) {
    this.companyForm = this.formBuilder.group({
      name: [''],
      document: [''],
      companyAddresses: this.formBuilder.array([
        this.createAddress()
      ])
    });
  }



  createAddress() {
    return this.formBuilder.group({
      street: [''],
      neighborhood: [''],
      city: [''],
      postalCode: [''],
      country: [''],
      companyTelephones: this.formBuilder.array([]) // Inicializa o FormArray vazio para os telefones
    });
  }

  get name() {
    return this.companyForm.get('name')!;
  }
  get document() {
    return this.companyForm.get('document')!;
  }
  get street() {
    return this.companyForm.get('street')!;
  }
  get neighborhood() {
    return this.companyForm.get('neighborhood')!;
  }
  get city() {
    return this.companyForm.get('city')!;
  }
  get postalCode() {
    return this.companyForm.get('postalCode')!;
  }
  get country() {
    return this.companyForm.get('country')!;
  }
  get phoneNumber() {
    return this.companyForm.get('phoneNumber')!;
  }

  removeAddress(index: number) {
    const control = <FormArray>this.companyForm.controls['companyAddresses'];
    control.removeAt(index);
  }


  addAddress() {
    const addresses = this.companyForm.get('companyAddresses') as FormArray;
    addresses.push(this.createAddress());
  }

  addPhone(address: AbstractControl) {
    const phones = address.get('companyTelephones') as FormArray;
    phones.push(this.formBuilder.group({
      phoneNumber: [''],
    }));
  }

  removePhone(address: AbstractControl, phoneIndex: number) {
    const phones = address.get('companyTelephones') as FormArray;
    phones.removeAt(phoneIndex);
  }

  getAddressesControls() {
    return (this.companyForm.get('companyAddresses') as FormArray).controls;
  }
  
  getPhonesControls(address: AbstractControl) {
    return (address.get('companyTelephones') as FormArray).controls;
  }

  submit() {
    // if (this.companyForm.invalid) {
    //   return;
    // }

    // console.log(this.companyForm.value);

    this.onSubmit.emit(this.companyForm.value);
  }
}