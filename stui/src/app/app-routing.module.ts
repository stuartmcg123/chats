import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AutoLoginAllRoutesGuard } from 'angular-auth-oidc-client';

const routes: Routes = [
  { path: 'messages', loadChildren: () => import('./messages/messages.module').then(c => c.MessagesModule), canActivate: [AutoLoginAllRoutesGuard] },
  { path: 'landing', loadChildren: () => import('./landing/landing.module').then(c => c.LandingModule), canActivate: [AutoLoginAllRoutesGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
