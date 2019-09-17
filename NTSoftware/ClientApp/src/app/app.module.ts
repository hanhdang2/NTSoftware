import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {
  NbThemeModule,
  NbLayoutModule,
  NbCardModule,
  NbIconModule
} from '@nebular/theme';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { AuthService } from './common/services/auth.service';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import {
  NbPasswordAuthStrategy,
  NbAuthModule,
  NbAuthService
} from '@nebular/auth';
import { NbEvaIconsModule } from '@nebular/eva-icons';
import { AdminGuard } from './guards/admin.guard';
import { CompanyGuard } from './guards/company.guard';
import { EmployeeGuard } from './guards/employee.guard';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AdminComponent } from './layout/admin/admin.component';
import { CompanyComponent } from './layout/company/company.component';
import { EmployeeComponent } from './layout/employee/employee.component';
import { NotFoundComponent } from './layout/not-found/not-found.component';
export function getToken() {
  return localStorage.getItem('currentUser');
}
@NgModule({
  declarations: [AppComponent, AdminComponent, CompanyComponent, EmployeeComponent, NotFoundComponent],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    NbEvaIconsModule,
    NbCardModule,
    NbIconModule,
    NbAuthModule.forRoot({
      strategies: [
        NbPasswordAuthStrategy.setup({
          name: 'email',
        })
      ],
      forms: {
        validation: {
          password: {
            required: true,
            minLength: 8,
            maxLength: 50,
          },
          code: {
            required: true,
          }
        },
      }
    }),
    BrowserAnimationsModule,
    NbThemeModule.forRoot({ name: 'default' }),
    NbLayoutModule,
    JwtModule.forRoot({config: {
      throwNoTokenError: false,
      tokenGetter: getToken,
      whitelistedDomains: ['localhost:4567']
    }}),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  providers: [AuthService, AdminGuard, CompanyGuard, EmployeeGuard],
  bootstrap: [AppComponent],
  exports: [TranslateModule]
})
export class AppModule {}
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, '../assets/i18n/', '.json');
}
