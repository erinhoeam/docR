import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [CommonModule,
             ReactiveFormsModule,
             RouterModule
            ],
  
    exports: [CommonModule,
              ReactiveFormsModule
              
            ],
  
    declarations: [],
    providers:[]
  })
  export class SharedModule { }