import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NbAuthComponent } from '@nebular/auth';
import { AuthComponent } from './auth.component'
import { LoginComponent } from './login/login.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
const routes: Routes = [
  {
    path: '',
    component: AuthComponent,
    children: [
      {
        path: '',
       redirectTo:'login', // <---
      },
      {
        path: 'login',
        component: LoginComponent, // <---
      },
      {path: 'reset-password',
      component: ResetPasswordComponent, // <---

      }
    ],  // <---
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],

})
export class AuthRoutingModule { }
