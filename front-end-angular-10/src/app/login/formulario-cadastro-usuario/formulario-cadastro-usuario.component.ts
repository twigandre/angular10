import { Component, OnInit, Injectable, ViewChild, Output, EventEmitter, ElementRef, Input } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from '@angular/router';
import { EnumRestResponse } from '../../shared/enums/EnumRestResponse';

@Component({
  selector: 'app-login',
  templateUrl: './formulario-cadastro-usuario.component.html'
})
export class FormularioCadastroUsuarioComponent implements OnInit {

  login : string;
  senha : string;
  parametros: any = {};
  statusLogin : number;
  
  informacao : string = ""

  constructor(
    private route: ActivatedRoute,
	  private router: Router		
    ) { }

  ngOnInit(){
  }

  validar(){
    

  }

  voltar(){
    var rota: string = "/novo-usuario";
    this.router.navigate([rota]);
  }
  
 
}
