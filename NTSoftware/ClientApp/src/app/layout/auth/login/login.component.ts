import { Component, OnInit, ChangeDetectorRef, Inject } from '@angular/core';
import { Router } from '@angular/router'
import { NbLoginComponent } from '@nebular/auth';
import { TranslateService } from '@ngx-translate/core';
import { AuthService } from '../../../common/services/auth.service';
import { NbAuthService,NB_AUTH_OPTIONS } from '@nebular/auth'
import { API_LOGIN } from '../../../constants/api/authen';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent  extends NbLoginComponent implements OnInit {
  ngOnInit() {

  }
  // tslint:disable-next-line:max-line-length
  constructor(private authService: AuthService, public translate: TranslateService, public service: NbAuthService, @Inject(NB_AUTH_OPTIONS) public options = {}, public cd: ChangeDetectorRef,  public router: Router) {
      super(service, options, cd, router);
      translate.setDefaultLang('vi');
    }
  login(){
    this.authService.onLogin(API_LOGIN, this.user).subscribe(res => {
      console.log(res);
    });
  }

}
