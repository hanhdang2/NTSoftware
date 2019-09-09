import { Component, OnInit, ChangeDetectorRef,Inject } from '@angular/core';
import { Router } from '@angular/router'
import { NbLoginComponent } from '@nebular/auth';
import { TranslateService } from '@ngx-translate/core';
import { NbAuthService,NB_AUTH_OPTIONS } from '@nebular/auth'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent  extends NbLoginComponent implements OnInit {
  ngOnInit() {

  }
  // tslint:disable-next-line:max-line-length
  constructor(public translate: TranslateService, public service: NbAuthService, @Inject(NB_AUTH_OPTIONS) public options = {}, public cd: ChangeDetectorRef,  public router: Router) {
      super(service, options, cd, router);
      translate.setDefaultLang('vi');
      translate.use('vi');

    }
  login(){
  }

}
