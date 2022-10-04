import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.scss']
})
export class LandingComponent implements OnInit {

  constructor(private oidc: OidcSecurityService) { }

  ngOnInit(): void {
  }

  login() {
    this.oidc
    .authorize();
  }
}
