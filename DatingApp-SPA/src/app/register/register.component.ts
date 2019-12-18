import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { error } from 'util';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Output() canselRegister = new EventEmitter();

  model: any = {};
  isRegistered: boolean = false;
  constructor(private authService: AuthService,
    private alertify: AlertifyService) { }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.model).subscribe(() => {
      this.alertify.success('Register Successful');
      this.isRegistered=true;
    }, error => {
      this.alertify.error(error);
    })
  }

  cansel() {
    this.canselRegister.emit(false);
  }

  registerToggle() {

  }
}
