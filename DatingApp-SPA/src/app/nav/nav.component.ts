import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {}

  constructor(public authService: AuthService,
    private alertify: AlertifyService,
    private router: Router) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Success!');
    }, error => {
      this.alertify.error(error);
    }), () => {
      this.router.navigate(['/members']);
    }
  }

  loggedIn() {
    if(!this.authService.loggedIn()){
      return true;
    }
    return false
  }

  logout() {
    localStorage.removeItem('token');
    this.alertify.message('Logout!');
    this.router.navigate(['/home']);
  }

}
