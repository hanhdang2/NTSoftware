import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './layout/login/login.component';
import { AdminComponent } from './layout/admin/admin.component';
import { CompanyComponent } from './layout/company/company.component';
import { EmployeeComponent } from './layout/employee/employee.component';

import { AdminGuard } from './guards/admin.guard';
import { CompanyGuard } from './guards/company.guard';
import { EmployeeGuard } from './guards/employee.guard';
import {
  ADMIN_ROUTE_NAME,
  LOGIN_ROUTE_NAME,
  COMPANY_ROUTE_NAME,
  EMPLOYEE_ROUTE_NAME
} from './constants/routes/index';

const routes: Routes = [
  {
    path: '',
    redirectTo: LOGIN_ROUTE_NAME,
    pathMatch: 'full'
  },
  {
    path: LOGIN_ROUTE_NAME,
    component: LoginComponent
  },
  {
    path: ADMIN_ROUTE_NAME,
    component: AdminComponent,
    canActivate: [AdminGuard]
  },
  {
    path: COMPANY_ROUTE_NAME,
    component: CompanyComponent,
    canActivate: [CompanyGuard]
  },
  {
    path: EMPLOYEE_ROUTE_NAME,
    component: EmployeeComponent,
    canActivate: [EmployeeGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
