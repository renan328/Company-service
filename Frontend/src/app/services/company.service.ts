import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs'
import { Company } from '../Company';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  private apiUrl = 'https://localhost:7287/api/Company'

  constructor(private http: HttpClient) { }

  getAll(): Observable<Company[]> {
    return this.http.get<Company[]>(this.apiUrl)
  }

  getCompany(id: number): Observable<Company> {
    return this.http.get<Company>(`${this.apiUrl}/${id}`);
  }

}
