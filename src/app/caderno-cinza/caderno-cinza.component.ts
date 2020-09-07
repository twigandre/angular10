import { Component, OnInit, Injectable, ViewChild, Output, EventEmitter, ElementRef, Input } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-caderno-cinza',
  templateUrl: './caderno-cinza.component.html',
  styleUrls: ['./caderno-cinza.component.css']
})
export class CadernoCinzaComponent implements OnInit {

  parametros : any = {}

  @ViewChild('appModalCadernoCinza') appModalCadernoCinza;

  constructor() { }

  ngOnInit(): void { }
  
  novo(){
    this.appModalCadernoCinza.abrir();
  }

}
