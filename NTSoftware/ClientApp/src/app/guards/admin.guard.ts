import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { TOKEN_ADMIN } from '../constants/localStorageKey/index';
import { LOGIN_ROUTE_NAME } from '../constants/routes/index';
@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {
  constructor(private jwtHelper: JwtHelperService, private router: Router) {
  }
  async canActivate() {
    const token = localStorage.getItem(TOKEN_ADMIN);
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    this.router.navigate([LOGIN_ROUTE_NAME]);
    return false;
  }

}
