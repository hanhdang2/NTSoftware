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
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
export function getToken() {
  return localStorage.getItem('currentUser');
}
@NgModule({
  declarations: [AppComponent],
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
            minLength: 4,
            maxLength: 50,
          },
          code:{
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
  providers: [AuthService],
  bootstrap: [AppComponent],
  exports: [TranslateModule]
})
export class AppModule {}
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, '../assets/i18n/', '.json');
}
