import { NgModule } from '@angular/core';
import { AuthModule } from 'angular-auth-oidc-client';


@NgModule({
    imports: [AuthModule.forRoot({
        config: {
              authority: 'https://identity.stuartmcgillivray.com',
              redirectUrl: "http://localhost:4200/callback",
              postLogoutRedirectUri: "http://localhost:4200",
              clientId: 'bestie-chat',
              scope: 'openid profile offline_access',
              responseType: 'code',
              silentRenew: true,
              useRefreshToken: true,
              renewTimeBeforeTokenExpiresInSeconds: 30,
          }
      })],
    exports: [AuthModule],
})
export class AuthConfigModule {}
