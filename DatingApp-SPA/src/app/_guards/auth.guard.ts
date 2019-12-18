import { Injectable } from '@angular/core';
import { CanActivate,Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private authService:AuthService,
    private router:Router,
    private alertService:AlertifyService
  ){}
  canActivate():boolean{
    if(this.authService.loggedIn()){
      this.alertService.error('You must be logged in!!!');
    this.router.navigate(['/home']);
      return false;
    }
    
    
    return true;
  }
  
}
