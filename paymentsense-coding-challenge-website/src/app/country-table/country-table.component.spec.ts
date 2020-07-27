import { async, ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CountryTableComponent } from './country-table.component';
import { PaymentsenseCodingChallengeApiService } from '../services';
import { MockPaymentsenseCodingChallengeApiService } from '../testing/mock-paymentsense-coding-challenge-api.service';

describe('CountryTableComponent', () => {
  let component: CountryTableComponent;
  let fixture: ComponentFixture<CountryTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CountryTableComponent ],
      imports: [
        NgbModule,
        FormsModule
      ],
      providers: [
        { provide: PaymentsenseCodingChallengeApiService, useClass: MockPaymentsenseCodingChallengeApiService }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CountryTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have a table with 10 rows', () => {
    const fixture = TestBed.createComponent(CountryTableComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelectorAll('tbody tr').length).toEqual(10);
  });

  it('should allow to expand row', fakeAsync(() => {
    const fixture = TestBed.createComponent(CountryTableComponent);
    fixture.autoDetectChanges();
    let row = fixture.debugElement.nativeElement.querySelectorAll('tbody tr')[3];
    row.click();
    fixture.autoDetectChanges();
    tick();
    expect(fixture.debugElement.nativeElement.querySelectorAll('tbody tr').length).toEqual(11);
    expect(fixture.debugElement.nativeElement.querySelectorAll('tbody tr')[4].textContent).toContain('C4 information');
  }));
});
