import { Component, OnInit, Injectable, ViewChild, Output, EventEmitter, ElementRef, Input } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from '@angular/router';
import { LoginService } from '../shared/service/login/login.service';
import { EnumRestResponse } from '../shared/enums/EnumRestResponse';
import { SocialAuthService,  } from "angularx-social-login";
import { FacebookLoginProvider, GoogleLoginProvider } from "angularx-social-login";
import { SignFacebookVM } from "../shared/view-models/SignFacebookVM";
import { SignGoogleVM } from "../shared/view-models/SignGoogleVM";
import { LoginVM } from '../shared/view-models/LoginVM';
import { ApplicationService } from '../shared/service/aplicationService/application.service';
import { UserVM } from '../shared/view-models/UserVM';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login : string;
  senha : string;
  parametros: any = {};
  statusLogin : number;
  isLoginGoogle : boolean = false;
  isLoginFacebook : boolean = false;
  informacao : string = ""
  serviceLogin = "User"; 

  sucesso : number = 200;
  erro : number = 400;
  user : any;

  constructor(
    private applicationService : ApplicationService,
    private route: ActivatedRoute,
    private LoginService: LoginService,
		private router: Router,
	  private authService: SocialAuthService,
  ) { 
    sessionStorage.clear();
  }

  ngOnInit(){

    this.authService.authState.subscribe(user => {
      
      if(this.isLoginGoogle || this.isLoginFacebook){

        this.isLoginGoogle ? 
        this.user = SignGoogleVM : 
        this.user = SignFacebookVM;
  
        var object = new UserVM();
        object.password = (this.isLoginGoogle) ? 'Autentication with Google' : 'Autentication with Facebook';
        object.email = user.email;

        this.applicationService.get<UserVM>(this.serviceLogin , object).subscribe((result: UserVM) => {
          if(result.requesteResponse == EnumRestResponse.SUCCESS){
            this.applicationService.setToken(user.authToken, result);
            this.navigate();
          }else if(result.requesteResponse == EnumRestResponse.USUARIO_AGUARDANDO_LIBERACAO_DO_ADMINISTRADOR){
            alert("Usuário aguardando liberação do administrador.");        
            return false;   
          } else if(result.requesteResponse == EnumRestResponse.NOT_FOUND) {
            alert("Usuário não encontrado, verifique login e senha e tente novamente.");
            return false;   
          } 
        });

      }
      
    });
  }

  validar(){
  
    if(!this.login)
    {
      this.informacao = "Preencha o Login";      
    } 
    else if(!this.senha)
    {
      this.informacao = "Preencha a Senha";    
    } 
    else
    {
     
      var objetoLogin = new UserVM();
      objetoLogin.email = this.login;
      objetoLogin.password = this.senha;      

      this.applicationService.get<UserVM>(this.serviceLogin , objetoLogin).subscribe((result: UserVM) => {
        if(result.requesteResponse == EnumRestResponse.SUCCESS){
          this.applicationService.setToken(result.token, result);
          this.navigate();
        }else if(result.requesteResponse == EnumRestResponse.USUARIO_AGUARDANDO_LIBERACAO_DO_ADMINISTRADOR){
          alert("Usuário aguardando liberação do administrador.");        
          return false;   
        } else if(result.requesteResponse == EnumRestResponse.NOT_FOUND) {
          alert("Usuário não encontrado, verifique login e senha e tente novamente.");
          return false;   
        } 
      });

    }

  }

  navigate(){
    this.router.navigate(["/sepractices4ml"]);
  }
  
  verificaInput(nomeInput){

    let existeCaractererEspecial : Boolean = false;

    let regexValidacao = new RegExp("^[a-zA-Z0-9-Z\b]+$");

    nomeInput == 'Login' ?
       existeCaractererEspecial = !regexValidacao.test(this.login.toString()) :
          existeCaractererEspecial = !regexValidacao.test(this.senha.toString());    

    if(existeCaractererEspecial)
    {
      nomeInput == 'Login' ? this.login = "" : this.senha = "";
      this.informacao = nomeInput + " nao pode conter caracterer especial, apenas numeros e letras sem acento."; 
      return false;
    }

    this.informacao = "";
    return true;
  }

  NovoUsuario(){
    var rota: string = "/novo-usuario";
    this.router.navigate([rota]);
  }

  loginWithGoogle(){
    this.isLoginGoogle = true;
    this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }
  
  loginWithFacebook(){
    this.isLoginFacebook = true;
    this.authService.signIn(FacebookLoginProvider.PROVIDER_ID);
   }

}
