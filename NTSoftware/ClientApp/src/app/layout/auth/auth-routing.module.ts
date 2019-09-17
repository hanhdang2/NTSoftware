import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthComponent } from './auth.component';
import { LoginComponent } from './login/login.component';
import { RequestPasswordComponent } from './request-password/request-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
const routes: Routes = [
  {
    path: '',
    component: AuthComponent,
    children: [
      {
        path: '',
        redirectTo: 'login' // <---
      },
      {
        path: 'login',
        component: LoginComponent // <---
      },
      {
        path: 'request-password',
        component: RequestPasswordComponent // <---
      },
      {
        path: 'reset-password',
        component: ResetPasswordComponent // <---,
      }
    ] // <---
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule {}
