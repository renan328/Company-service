export interface Company {
    id: number;
    name: string;
    document: string;
    createDate: string;
    updateDate: string;
    companyAddresses: CompanyAddress[];
  }
  
  export interface CompanyAddress {
    id: number;
    companyId: number;
    street: string;
    neighborhood: string;
    city: string;
    postalCode: string;
    country: string;
    createDate: string;
    updateDate: string;
    companyTelephones: CompanyTelephone[];
  }
  
  export interface CompanyTelephone {
    id: number;
    companyAddress: number;
    phoneNumber: string;
    createDate: string;
    updateDate: string;
  }
  