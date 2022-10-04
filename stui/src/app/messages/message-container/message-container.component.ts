import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-message-container',
  templateUrl: './message-container.component.html',
  styleUrls: ['./message-container.component.scss']
})
export class MessageContainerComponent implements OnInit {

  constructor(private oidc: OidcSecurityService) { }

  ngOnInit(): void {
  }
  logout() {
    this.oidc.logoff();
  }
}
