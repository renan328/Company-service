import { Component, EventEmitter, Input, Output, OnInit } from '@angular/core';
import { Company } from 'src/app/Company';
import { CompanyAddress } from 'src/app/CompanyAddress';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormArray,
  AbstractControl,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-form-company',
  templateUrl: './form-company.component.html',
  styleUrls: ['./form-company.component.css'],
})
export class FormCompanyComponent {
  @Output() onSubmit = new EventEmitter<Company>();
  @Input() companyData?: Company;
  @Input() btnText!: string;

  company: Company = {
    id: 0,
    name: '',
    document: '',
    companyAddresses: [],
  };

  companyForm!: FormGroup;

  constructor(private formBuilder: FormBuilder) {}

  ngOnInit() {
    this.companyForm = this.formBuilder.group({
      id: [0],
      name: ['', Validators.required],
      document: ['', [Validators.required, Validators.minLength(14)]],
      companyAddresses: this.formBuilder.array([this.createAddress()]),
    });

    if (this.companyData) {
      this.companyForm.patchValue({
        id: this.companyData.id,
        name: this.companyData.name,
        document: this.companyData.document,
      });

      const addressesArray = this.companyForm.get(
        'companyAddresses'
      ) as FormArray;
      addressesArray.clear();

      this.companyData.companyAddresses.forEach((address) => {
        const addressGroup = this.formBuilder.group({
          id: address.id,
          street: address.street,
          neighborhood: address.neighborhood,
          city: address.city,
          postalCode: address.postalCode,
          country: address.country,
          companyTelephones: this.formBuilder.array([]),
        });
        address.companyTelephones.forEach((phone) => {
          const phoneGroup = this.formBuilder.group({
            id: phone.id,
            phoneNumber: phone.phoneNumber,
          });
          const phonesArray = addressGroup.get(
            'companyTelephones'
          ) as FormArray;
          phonesArray.push(phoneGroup);
        });

        addressesArray.push(addressGroup);
      });
    }
  }

  createAddress() {
    return this.formBuilder.group({
      id: [0],
      street: ['', Validators.required],
      neighborhood: ['', Validators.required],
      city: ['', Validators.required],
      postalCode: ['', Validators.required],
      country: ['', Validators.required],
      companyTelephones: this.formBuilder.array([this.createPhone()]),
    });
  }

  createPhone() {
    return this.formBuilder.group({
      id: [0],
      phoneNumber: ['', [Validators.required, Validators.minLength(8)]],
    });
  }

  addAddress() {
    const addresses = this.companyForm.get('companyAddresses') as FormArray;
    addresses.push(this.createAddress());
  }

  removeAddress(index: number) {
    const control = <FormArray>this.companyForm.controls['companyAddresses'];
    control.removeAt(index);
  }

  addPhone(address: AbstractControl) {
    const phones = address.get('companyTelephones') as FormArray;
    phones.push(this.createPhone());
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

  submitForm() {
    if (this.companyForm.invalid) {
      return;
    }

    this.onSubmit.emit(this.companyForm.value);
  }
}
