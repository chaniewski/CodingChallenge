import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CountryTableComponent } from './country-table/country-table.component';

const routes: Routes = [
  { path: '', component: CountryTableComponent },
  { path: '**', component: CountryTableComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
