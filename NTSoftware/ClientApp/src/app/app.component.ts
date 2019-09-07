import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { UserService } from './common/services/user.service';
import { HttpParams } from '@angular/common/http';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'NTSoftware';
}
