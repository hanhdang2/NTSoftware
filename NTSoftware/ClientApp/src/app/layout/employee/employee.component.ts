import { Component, OnInit } from '@angular/core';
import { LANGUAGE_LOCAL } from 'src/app/constants/localStorageKey';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.scss']
})
export class EmployeeComponent implements OnInit {

  constructor(private route: Router) { }

  ngOnInit() {
  }
  onLogout() {
    this.clearToken();
    this.route.navigate(['auth'])
  }
  clearToken() {
    const language = localStorage.getItem(LANGUAGE_LOCAL);
    localStorage.clear();
    localStorage.setItem(LANGUAGE_LOCAL, language);
  }
}
