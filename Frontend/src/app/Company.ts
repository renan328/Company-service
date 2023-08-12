import { CompanyAddress } from "./CompanyAddress";
export interface Company {
  id?: number;
  name: string;
  document: string;
  createDate?: string;
  updateDate?: string;
  companyAddresses: CompanyAddress[];
}
