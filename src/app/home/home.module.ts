import { NgModule } from '@angular/core';

import { ProgressbarModule } from 'ngx-bootstrap/progressbar';

import { SharedModule } from './../shared/shared.module';
import { HomeRoutingModule } from './home.routing.module';
import { HomeMainComponent } from './home-main/home-main.component';
import { HomeComponent } from './home.component';

import { HomeService } from './../services/home.service';


@NgModule({
  imports: [SharedModule, HomeRoutingModule, ProgressbarModule.forRoot()],
  declarations: [HomeMainComponent, HomeComponent],
  exports: [HomeComponent],
  providers:[HomeService]  
})
export class HomeModule { }
