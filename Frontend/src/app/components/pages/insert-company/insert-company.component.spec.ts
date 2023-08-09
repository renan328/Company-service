import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsertCompanyComponent } from './insert-company.component';

describe('InsertCompanyComponent', () => {
  let component: InsertCompanyComponent;
  let fixture: ComponentFixture<InsertCompanyComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [InsertCompanyComponent]
    });
    fixture = TestBed.createComponent(InsertCompanyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
