import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AdminGuard } from './guards/admin.guard';
import { CompanyGuard } from './guards/company.guard';
import { EmployeeGuard } from './guards/employee.guard';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { JwtModule } from '@auth0/angular-jwt';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './layout/login/login.component';
import { AdminComponent } from './layout/admin/admin.component';
import { CompanyComponent } from './layout/company/company.component';
import { EmployeeComponent } from './layout/employee/employee.component';

@NgModule({
  declarations: [AppComponent, LoginComponent, AdminComponent, CompanyComponent, EmployeeComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    JwtModule.forRoot({ config: {} }),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  providers: [AdminGuard, CompanyGuard, EmployeeGuard],
  bootstrap: [AppComponent]
})
export class AppModule {}
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, '../assets/i18n/', '.json');
}
