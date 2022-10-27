import { NgModule } from '@angular/core';
import { AuthModule } from 'angular-auth-oidc-client';


@NgModule({
    imports: [AuthModule.forRoot({
        config: {
              authority: 'https://identity.stuartmcgillivray.com',
              redirectUrl: `${location.origin}/callback`,
              postLogoutRedirectUri: location.origin,
              clientId: 'bestie-chat-live',
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
