import { Component, OnInit } from '@angular/core';

import { ToastrService } from 'ngx-toastr';

import { HomeService } from './../services/home.service';
import { Documento } from '../model/documento';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  public fileList:FileList;
  public documentos: Documento[];
  public fileName:String;
  constructor(private homeService:HomeService, private toastr: ToastrService) { }

  ngOnInit() {
    setTimeout(() => this.toastr.success('sup'));
  }
  showSuccess() {
    this.toastr.success('Hello world!', 'Toastr fun!');
  }
  public fileChange(event) {
    this.fileList = event.target.files;
    
    if (this.fileList != null && this.fileList.length >0){

      let file = this.fileList[0];
      this.documentos = null;
      this.fileName = "";
      this.homeService.upload(file)
            .subscribe(
            result => { 
              this.documentos = result; 
              this.fileName = `https://docrstorage.blob.core.windows.net/images/${file.name}`;
              //this.toastr.clear();
            },
            error => { console.log(error) });
    }
  }
}
