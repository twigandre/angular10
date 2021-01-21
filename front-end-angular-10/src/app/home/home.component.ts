import { Component, OnInit, Injectable, ViewChild, Output, EventEmitter, ElementRef, Input } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {

  isCadernoCinza : boolean = false;
  isDespesasGerais : boolean = false;
  isCompras : boolean = false;
  isFaturamento : boolean = false;
  isItau : boolean = false;

  constructor( 
    private route: ActivatedRoute,
		private router: Router) { }

  ngOnInit(): void {
    this.selecionaItem(1);
  }

  selecionaItem(opcao){    
    this.desativaItens();
    if(opcao == 1){
      this.isCadernoCinza = true;
    }
    if(opcao == 2){
      this.isDespesasGerais = true;
    }
    if(opcao == 3){
      this.isCompras = true;
    }
    if(opcao == 4 ){
      this.isFaturamento = true;
    }
    if(opcao == 5){
      this.isItau = true;
    }
  }

  desativaItens(){
    this.isCadernoCinza = false;
    this.isDespesasGerais = false;
    this.isCompras = false;
    this.isFaturamento = false;
    this.isItau = false;
  }
  

}
