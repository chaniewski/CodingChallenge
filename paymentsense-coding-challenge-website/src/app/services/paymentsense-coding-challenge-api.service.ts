import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject, Subject, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Country } from '../models/Country';
import { map, switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PaymentsenseCodingChallengeApiService {
  private _search$ = new Subject<void>();
  private _countries$ = new BehaviorSubject<Country[]>([]);
  private _total$ = new BehaviorSubject<number>(0);
  private _page = 1;
  private _pageSize = 15;

  constructor(private httpClient: HttpClient) {
    this._search$.pipe(
      switchMap(() => this._search()),
    ).subscribe(result => {
      this._countries$.next(result.slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize));
      this._total$.next(result.length);
    });

    this._search$.next();
  }

  public getHealth(): Observable<string> {
    return this.httpClient.get('https://localhost:44356/health', { responseType: 'text' });
  }

  get countries$() { return this._countries$.asObservable(); }
  get total$() { return this._total$.asObservable(); }

  get page() { return this._page; }
  set page(page: number) { 
    this._page = page;
    this._search$.next();
  }

  get pageSize() { return this._pageSize; }
  set pageSize(pageSize: number) { 
    this._pageSize = pageSize;
    this._search$.next();
  }

  get searchTerm() { return this._searchTerm; }
  set searchTerm(searchTerm: string) { 
    this._searchTerm = searchTerm; 
    this._search$.next();
  }

  private _searchTerm: string = '';

  private _search(): Observable<Country[]> {
    return this.httpClient.get<Country[]>('https://localhost:44356/countries', {responseType: 'json'})
        .pipe(map(items => items.filter(item => this.searchTerm==='' || item.name.toLowerCase().indexOf(this.searchTerm.toLowerCase()) > -1)));
  }
}
