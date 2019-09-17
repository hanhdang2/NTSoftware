import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LANGUAGE_LOCAL } from 'src/app/constants/localStorageKey';

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.scss']
})
export class CompanyComponent implements OnInit {

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
