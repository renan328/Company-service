import { CompanyTelephone } from "./CompanyTelephone";

export interface CompanyAddress {
    id?: number;
    companyId?: number;
    street: string;
    neighborhood: string;
    city: string;
    postalCode: string;
    country: string;
    createDate?: string;
    updateDate?: string;
    companyTelephones: CompanyTelephone[];
}