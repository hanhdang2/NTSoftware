import { Component, OnInit, ChangeDetectorRef, Inject } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { NbLoginComponent } from '@nebular/auth';
import { TranslateService } from '@ngx-translate/core';
import { AuthService } from '../../../common/services/auth.service';
import { API_RESET_PASSWORD } from '../../../constants/api/authen';
import { NbAuthService, NB_AUTH_OPTIONS } from '@nebular/auth';
import { NbResetPasswordComponent } from '@nebular/auth';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent extends NbResetPasswordComponent
  implements OnInit {
  token = '';
  constructor(
    private authService: AuthService,
    public translate: TranslateService,
    public service: NbAuthService,
    @Inject(NB_AUTH_OPTIONS) public options = {},
    public cd: ChangeDetectorRef,
    public router: Router,
    private activeRoute: ActivatedRoute
  ) {
    super(service, options, cd, router);
    translate.setDefaultLang('vi');
  }
  resetPass() {
    this.submitted = true;
    this.errors = null;
    this.messages = null;
    console.log(this.user);
    this.authService.onResetPassword(API_RESET_PASSWORD, this.user).subscribe(
      (res: any) => {
        this.submitted = false;
        if (res.success === true) {
          this.messages = [
            this.translate.instant('auth.resetPassword.resetSuccess')
          ];
        } else {
          console.log(res);
          this.errors = [res.message];
        }
      },
      error => {
        this.submitted = false;
        console.log(error);
      }
    );
  }
  ngOnInit() {
    this.activeRoute.queryParams.subscribe(params => {
      if (params.code && params.userId) {
        let tokenparam = '';
        const token = params.code.toString().split(' ');
        token.forEach(item => {
          tokenparam += item + '+';
        });
        this.user.userId = params.userId;
        this.user.tokenCode = tokenparam.slice(0, tokenparam.length - 1);
      }
    });
  }
}
