import { Component, OnInit, Injectable, ViewChild, Output, EventEmitter, ElementRef, Input } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login : string;
  senha : string;
  parametros: any = {};

  constructor(
    private route: ActivatedRoute,
		private router: Router		
  ) { }

  ngOnInit(){
  }

  validar(){

    if(this.login != "admin" || !this.login){
      alert("Login inválido");      
    } 
    else if(this.senha != "admin" || !this.senha){
      alert("Senha inválida");    
    } 
    else{
      this.navigate();
    }

  }

  navigate(){
    var rota: any = "/home";
    this.router.navigate([rota]);
  }


}
