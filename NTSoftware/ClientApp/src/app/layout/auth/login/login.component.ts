import { Component, OnInit, ChangeDetectorRef, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { NbLoginComponent } from '@nebular/auth';
import { TranslateService } from '@ngx-translate/core';
import { AuthService } from '../../../common/services/auth.service';
import { NbAuthService, NB_AUTH_OPTIONS } from '@nebular/auth';
import { API_LOGIN } from '../../../constants/api/authen';
import { JwtHelperService } from '@auth0/angular-jwt';
import {
  ADMIN_ROLE,
  COMPANY_ROLE,
  EMPLOYEE_ROLE
} from '../../../constants/roles/index';
import {
  TOKEN_ADMIN,
  TOKEN_COMPANY,
  TOKEN_EMPLOYEE,
  LANGUAGE_LOCAL
} from '../../../constants/localStorageKey/index';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent extends NbLoginComponent implements OnInit {
  constructor(
    private JwtHelper: JwtHelperService,
    private authService: AuthService,
    public translate: TranslateService,
    public service: NbAuthService,
    @Inject(NB_AUTH_OPTIONS) public options = {},
    public cd: ChangeDetectorRef,
    public router: Router
  ) {
    super(service, options, cd, router);
    translate.setDefaultLang('vi');
  }

  ngOnInit() {
    this.getToken();
  }
  getToken() {
    const companyToken = localStorage.getItem(TOKEN_COMPANY);
    const adminToken = localStorage.getItem(TOKEN_ADMIN);
    const employeeToken = localStorage.getItem(TOKEN_EMPLOYEE);
    if (companyToken) {
      this.router.navigate(['company']);
    } else if (adminToken) {
      this.router.navigate(['admin']);
    } else if (employeeToken) {
      this.router.navigate(['admin']);
    }
  }
  // tslint:disable-next-line:max-line-length

  clearToken() {
    const language = localStorage.getItem(LANGUAGE_LOCAL);
    localStorage.clear();
    localStorage.setItem(LANGUAGE_LOCAL, language);
  }
  login() {
    this.submitted = true;
    this.errors = null;
    this.messages = null;
    this.authService.onLogin(API_LOGIN, this.user).subscribe(
      (res: any) => {
        this.submitted = false;
        if (res.success === false) {
          this.errors = [res.message];
        } else {
          const user = this.JwtHelper.decodeToken(res.data);

          switch (user.UserType.toString()) {
            case 'AdminNT':
              localStorage.setItem(TOKEN_ADMIN, res.data);
              this.router.navigate(['admin']);
              break;
            case 'Employee':
              localStorage.setItem(TOKEN_EMPLOYEE, res.data);
              this.router.navigate(['employee']);
              break;
            case 'AdminCompany':
              localStorage.setItem(TOKEN_COMPANY, res.data);
              this.router.navigate(['company']);
              break;
            default:
                this.errors = [this.translate.instant('auth.login.anError')];
                return;
          }
        }
      },
      error => {
        this.submitted = false;
        this.errors = [this.translate.instant('auth.login.anError')];
      }
    );
  }
}
