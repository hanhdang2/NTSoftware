import { Component, OnInit } from '@angular/core';
import { NbAuthComponent } from '@nebular/auth';
import { NbAuthService } from '@nebular/auth';
import { Location } from '@angular/common'

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent extends NbAuthComponent implements OnInit {

  ngOnInit() {
  }

}
