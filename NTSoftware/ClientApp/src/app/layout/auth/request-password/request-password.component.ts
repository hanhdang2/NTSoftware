import { Component, OnInit,ChangeDetectorRef, Inject } from '@angular/core';
import { Router } from '@angular/router'
import { NbRequestPasswordComponent } from '@nebular/auth';
import { NbLoginComponent } from '@nebular/auth';
import { TranslateService } from '@ngx-translate/core';
import { NbAuthService, NB_AUTH_OPTIONS } from '@nebular/auth'
@Component({
  selector: 'app-reset-password',
  templateUrl: './request-password.component.html',
  styleUrls: ['./request-password.component.scss']
})
export class RequestPasswordComponent extends NbRequestPasswordComponent implements OnInit {
  ngOnInit() {
  }
  constructor( public translate: TranslateService, public service: NbAuthService, @Inject(NB_AUTH_OPTIONS) public options = {}, public cd: ChangeDetectorRef,  public router: Router) {
    super(service, options, cd, router);

  }
}
