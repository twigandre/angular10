import { Component, OnInit, Injectable, ViewChild, Output, EventEmitter, ElementRef, Input } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from '@angular/router';
import { LoginService } from '../shared/service/login/login.service';

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
  
  informacao : string = ""

  sucesso : number = 200;
  erro : number = 400;


  constructor(
    private route: ActivatedRoute,
    private LoginService: LoginService,
		private router: Router		
  ) { }

  ngOnInit(){
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
     
      var objetoLogin : any = {
        login : this.login,
        senha : this.senha
      }

      this.LoginService.logar(objetoLogin).subscribe((result: any) => 
      {
          if(!result){
            alert("Falha ao tentar efetura login, Erro no Servidor!")
          } if(result == 400) {
            alert("Usuário nao Encontrado! Verifique o Login e Senha e Tente Novamente")
          }else if(result == 200){
            this.navigate();
          }              
      });
    }

  }

  navigate(){
    var rota: any = "/home";
    this.router.navigate([rota]);
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


}
