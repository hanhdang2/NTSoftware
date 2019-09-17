import { Component, OnInit } from '@angular/core';
import { LANGUAGE_LOCAL } from 'src/app/constants/localStorageKey';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

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
