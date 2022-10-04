import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'messages', loadChildren: () => import('./messages/messages.module').then(c => c.MessagesModule) },
  { path: 'landing', loadChildren: () => import('./landing/landing.module').then(c => c.LandingModule) }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
