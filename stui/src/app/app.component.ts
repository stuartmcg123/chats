import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'stui';
  constructor(
    private oidcService: OidcSecurityService,
    private router: Router) {

  }

  ngOnInit(): void {
    this.oidcService
      .checkAuth()
      .subscribe(({ isAuthenticated, userData, accessToken, idToken }) => {
        console.log(isAuthenticated);
        isAuthenticated ? this.router.navigate(['messages']) : this.router.navigate(['landing']);
      });

    // this.oidcService.logoff();
  }
}
