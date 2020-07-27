import { Component, OnInit } from '@angular/core';
import { Country } from '../models/Country';
import { PaymentsenseCodingChallengeApiService } from '../services';
import { take } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-country-table',
  templateUrl: './country-table.component.html',
  styleUrls: ['./country-table.component.scss']
})
export class CountryTableComponent implements OnInit {

  public countries$: Observable<Country[]>;
  public total$: Observable<number>;

  public selectedCountry = null;
  public Math: any = Math;

  constructor(public service: PaymentsenseCodingChallengeApiService) { 
    this.countries$ = this.service.countries$;
    this.total$ = this.service.total$;
  }

  refreshCountries() {
  }

  ngOnInit() {
  }

}
