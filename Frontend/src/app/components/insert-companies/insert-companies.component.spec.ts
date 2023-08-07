import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsertCompaniesComponent } from './insert-companies.component';

describe('InsertCompaniesComponent', () => {
  let component: InsertCompaniesComponent;
  let fixture: ComponentFixture<InsertCompaniesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [InsertCompaniesComponent]
    });
    fixture = TestBed.createComponent(InsertCompaniesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
