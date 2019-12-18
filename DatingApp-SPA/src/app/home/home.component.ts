import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  
  registerMode=false;

  constructor() { }

  ngOnInit() {
  }

  reigisterToggle(){
    this.registerMode=true;
  }  

  canselRegisterMode(registerMode:boolean){
    this.registerMode=!registerMode;
  }

}
