import { Component, OnInit, ChangeDetectorRef, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../common/services/auth.service';
import { NbRequestPasswordComponent } from '@nebular/auth';
import { NbLoginComponent } from '@nebular/auth';
import { TranslateService } from '@ngx-translate/core';
import { API_REQUEST_PASSWORD } from '../../../constants/api/authen';
import { NbAuthService, NB_AUTH_OPTIONS } from '@nebular/auth';
@Component({
  selector: 'app-reset-password',
  templateUrl: './request-password.component.html',
  styleUrls: ['./request-password.component.scss']
})
export class RequestPasswordComponent extends NbRequestPasswordComponent
  implements OnInit {
  ngOnInit() {}
  // tslint:disable-next-line:max-line-length
  constructor(
    private authService: AuthService,
    public translate: TranslateService,
    public service: NbAuthService,
    @Inject(NB_AUTH_OPTIONS) public options = {},
    public cd: ChangeDetectorRef,
    public router: Router
  ) {
    super(service, options, cd, router);
  }
  requestPass() {
    this.submitted = true;
    this.errors = null;
    this.messages = null;
    this.authService
      .onRequestPassword(API_REQUEST_PASSWORD, this.user)
      .subscribe(
        (res: any) => {
          this.submitted = false;
          console.log(res);
          if (res.success === false) {
            this.errors = [res.message];
          } else {
            if (res.errorCode === 6) {
              this.messages = [
                this.translate.instant('auth.requestPassword.sendEmailSucces')
              ];
            } else {
              this.errors = [
                this.translate.instant('auth.requestPassword.sendEmailFailed')
              ];
            }
          }
        },
        error => {
          this.submitted = false;
          console.log(error);
        }
      );
  }
}
