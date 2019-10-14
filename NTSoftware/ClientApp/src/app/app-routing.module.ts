import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeGuard } from './guards/employee.guard';
import { CompanyGuard } from './guards/company.guard';
import { AdminComponent } from './layout/admin/admin.component';
import { CompanyComponent } from './layout/company/company.component';
import { EmployeeComponent } from './layout/employee/employee.component';
import { NotFoundComponent } from './layout/not-found/not-found.component';
import { AdminGuard } from './guards/admin.guard';
const routes: Routes = [
  {
    path: '',
    redirectTo: 'auth',
    pathMatch: 'full'
  },
  {
    path: 'auth',
    loadChildren: '../app/layout/auth/auth.module#AuthModule'
  },
  {
    path: 'admin',
    component: AdminComponent,
    canActivate: [AdminGuard]
  },
  {
    path: 'company',
    component: CompanyComponent,
    canActivate: [CompanyGuard]
  },
  {
    path: 'employee',
    component: EmployeeComponent,
    canActivate: [EmployeeGuard]
  },
  {
    path: '**',
    component: NotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
