import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

//import { AuthService } from './../shared/auth.service';

import { HomeMainComponent } from './home-main/home-main.component';
import { HomeComponent } from './home.component';
//import { AcessoNegadoComponent } from './acesso-negado/acesso-negado.component';

const ROUTES: Routes = [
    { path: '', component: HomeMainComponent,
    children: [ 
      { path: 'inicial', component: HomeComponent }
    ]},
];

@NgModule({
  imports: [RouterModule.forChild(ROUTES)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }