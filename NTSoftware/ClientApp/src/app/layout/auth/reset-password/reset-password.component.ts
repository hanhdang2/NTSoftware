import { Component, OnInit, ChangeDetectorRef, Inject } from '@angular/core';
import { Router } from '@angular/router'
import { NbLoginComponent } from '@nebular/auth';
import { TranslateService } from '@ngx-translate/core';
import { AuthService } from '../../../common/services/auth.service';
import { NbAuthService,NB_AUTH_OPTIONS } from '@nebular/auth'
import { NbResetPasswordComponent } from '@nebular/auth';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent extends NbResetPasswordComponent implements OnInit {
  param = {email: 'ngokprao121@gmail.com'};
  constructor(private authService: AuthService, public translate: TranslateService, public service: NbAuthService, @Inject(NB_AUTH_OPTIONS) public options = {}, public cd: ChangeDetectorRef,  public router: Router) {
    super(service, options, cd, router);
    translate.setDefaultLang('vi');
  }
  resetPass(){
    console.log(this.user)
  }
  ngOnInit() {
  }

}
