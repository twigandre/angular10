//SERVICES
import {ApplicationService} from '../app/shared/service/aplicationService/application.service';
import {ConverterService} from '../app/shared/service/convertService/convert.service';
import {InterceptorService} from '../app/shared/loader/interceptor.service';
import {LoaderService} from '../app/shared/loader/loader.service';

//  MODULES
import { MatProgressBarModule } from '@angular/material/progress-bar'
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner'
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';

// AUTENTICAÇÃO EM REDES SOCIAIS
import { SocialLoginModule, SocialAuthServiceConfig } from 'angularx-social-login';
import { GoogleLoginProvider, FacebookLoginProvider} from 'angularx-social-login';

//  COMPONENTS
import { LoginComponent } from './login/login.component';
import { FormularioCadastroUsuarioComponent } from './login/formulario-cadastro-usuario/formulario-cadastro-usuario.component';
import { ComentPracticesComponent } from './practices/comentarios/comment-practices.component'; 
import { MenuComponent } from './home/menu/menu.component';
import { SharedModule } from './shared/shared.module';
import { PracticesComponent } from './practices/practices.component';
import { GridPracticesComponent } from './practices/grid/grid-practices.component';
import { ManterPracticesComponent } from './practices/manter-practices/manter-practices.component'
import { HomeComponent } from './home/home.component';
import { MembersComponent } from './members/members.component';
import { GridMembersComponent } from './members/grid/grid-members.component';
import { ManterMembersComponent } from './members/manter-mermbers/manter-members.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NotificacoesComponent } from './notificacoes/notificacoes.component';

@NgModule({

  declarations: [
    AppComponent,
    LoginComponent,
    ComentPracticesComponent,
    FormularioCadastroUsuarioComponent,
    MenuComponent,
    PracticesComponent,
    GridPracticesComponent,
    HomeComponent,
    ManterPracticesComponent,
    MembersComponent,
    GridMembersComponent,
    ManterMembersComponent,
    NotificacoesComponent
  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    SharedModule,
    NgxPaginationModule,
    SocialLoginModule,
    BrowserAnimationsModule,
    MatProgressBarModule,
    MatProgressSpinnerModule
  ],

  providers: [
    ApplicationService,
    ConverterService,
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider('624796833023-clhjgupm0pu6vgga7k5i5bsfp6qp6egh.apps.googleusercontent.com')
          },
          {
            id: FacebookLoginProvider.PROVIDER_ID,
            provider: new FacebookLoginProvider('785092785421932')
          }
        ]
      } as SocialAuthServiceConfig,
    },
    {provide:HTTP_INTERCEPTORS , useClass: InterceptorService, multi: true}
  ],

  bootstrap: [AppComponent]
})
export class AppModule { }
