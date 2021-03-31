import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { FormularioCadastroUsuarioComponent } from './login/formulario-cadastro-usuario/formulario-cadastro-usuario.component';
import { HomeComponent } from './home/home.component';
import { PracticesComponent } from './practices/practices.component';
import { ManterPracticesComponent } from './practices/manter-practices/manter-practices.component';
import { MembersComponent } from './members/members.component';
import { ManterMembersComponent } from "./members/manter-mermbers/manter-members.component";
import { NotificacoesComponent } from "./notificacoes/notificacoes.component";

const routes: Routes = [
  { path: "login", component: LoginComponent },
  { path: 'new-user', component: FormularioCadastroUsuarioComponent},
  { 
    path: "sepractices4ml", component: HomeComponent , 
      children:[	 
         
        //practices
        {path: "practices", component: PracticesComponent},
        {path: "practices/new", component: ManterPracticesComponent},
        {path: "practices/:idPractice/view", component: ManterPracticesComponent},
        {path: "practices/:idPractice/edit", component: ManterPracticesComponent},

        //members
        {path: "members", component: MembersComponent},
        {path: "members/new", component: ManterMembersComponent},
        {path: "members/:idUser/view", component: ManterMembersComponent},
        {path: "members/:idUser/edit", component: ManterMembersComponent},

        //notification
        {path: "notifications", component: NotificacoesComponent}

      ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
