import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public _jwtHelper = new JwtHelperService();
  public _decodedToken:any;

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(environment.base_url + 'auth/' + 'login', model)
      .pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            localStorage.setItem('token', user.token);
            this._decodedToken=this._jwtHelper.decodeToken(user.token);
          }
        })
      )
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return this._jwtHelper.isTokenExpired(token);
  }

  register(model: any) {
    return this.http.post(environment.base_url + 'auth/' + 'register', model);
  }
}
