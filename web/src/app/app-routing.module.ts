import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { UsersComponent } from "@pages/users/users.component";
import { LoginComponent } from "@pages/login/login.component";
import { NotfoundComponent } from "@pages/notfound/notfound.component";

const routes: Routes = [
  {
    path: '', component: LoginComponent,
  },
  {
    path: 'users', component: UsersComponent,
  },
  { path: 'notfound', component: NotfoundComponent },
  { path: '**', redirectTo: '/notfound' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
