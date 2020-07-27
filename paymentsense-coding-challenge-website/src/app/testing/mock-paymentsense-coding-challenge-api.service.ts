import { Injectable } from '@angular/core';
import { of, Observable } from 'rxjs';
import { Country } from '../models/Country';

@Injectable({
  providedIn: 'root'
})
export class MockPaymentsenseCodingChallengeApiService {
  public getHealth(): Observable<string> {
    return of('Healthy');
  }

  get countries$(): Observable<Country[]> {
    var countries: Country[] = [
      new Country({name: 'C1', population: 10}),
      new Country({name: 'C2', population: 20}),
      new Country({name: 'C3', population: 30}),
      new Country({name: 'C4', population: 40}),
      new Country({name: 'C5', population: 50}),
      new Country({name: 'C6', population: 60}),
      new Country({name: 'C7', population: 70}),
      new Country({name: 'C8', population: 80}),
      new Country({name: 'C9', population: 90}),
      new Country({name: 'C10', population: 100}),
    ];
    return of(countries);
  }

  get total$() { return of(10); }

  get pageSize() { return 15; }
  set pageSize(pageSize: number) {}

  get searchTerm() { return ''; }
  set searchTerm(searchTerm: string) { }
}
