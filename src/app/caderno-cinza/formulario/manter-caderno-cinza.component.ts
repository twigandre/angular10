import { Component, OnInit, Injectable, ViewChild, Output, EventEmitter, ElementRef, Input } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from "@angular/forms";

@Component({
  selector: 'app-manter-caderno-cinza',
  templateUrl: './manter-caderno-cinza.component.html',
  styleUrls: ['./manter-caderno-cinza.component.css']
})
export class ManterCadernoCinzaComponent implements OnInit {

  parametros : any = {}

  @ViewChild('appModalCadernoCinza') appModalCadernoCinza;
	@ViewChild('appModalCadernoCinzaBack') appModalCadernoCinzaBack;

  constructor() { }

  ngOnInit(): void { }

  abrir(){
    this.appModalCadernoCinza.nativeElement.style.display = 'block';
		this.appModalCadernoCinzaBack.nativeElement.style.display = 'block';
  }

  fechar(){
    this.appModalCadernoCinza.nativeElement.style.display = 'none';
		this.appModalCadernoCinzaBack.nativeElement.style.display = 'none';
  }

  validar(){
    
  }

}

