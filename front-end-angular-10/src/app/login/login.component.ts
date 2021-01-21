import { Component, OnInit, Injectable, ViewChild, Output, EventEmitter, ElementRef, Input } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from '@angular/router';
import { LoginService } from '../shared/service/login.service';

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

    if(!this.login){
      alert("Login inválido");      
    } 
    else if(!this.senha){
      alert("Senha inválida");    
    } 
    else{
     
      var objetoLogin : any = {
        login : this.login,
        senha : this.senha
      }

      this.LoginService.logar(objetoLogin).subscribe((result: any) => 
      {
          (result == 400 || !result) ? 
            alert("Usuário nao Encontrado!") :
              this.navigate();
      });
    }

  }

  navigate(){
    var rota: any = "/home";
    this.router.navigate([rota]);
  }
  
}
