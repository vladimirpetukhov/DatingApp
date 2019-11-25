import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { error } from 'util';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Output() canselRegister = new EventEmitter();

  model: any = {};
  isRegistered: boolean = false;
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.model).subscribe(() => {
      console.log('Register Successful');
    }, error => {
      console.log(error);
    })
  }

  cansel() {
    this.canselRegister.emit(false);
  }
  registerToggle() {

  }
}
