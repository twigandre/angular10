import { Component, OnInit, Injectable, ViewChild, Output, EventEmitter, ElementRef, Input } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from "@angular/forms";

@Component({
  selector: 'app-coment-practices',
  templateUrl: './comment-practices.component.html'
})
export class ComentPracticesComponent implements OnInit {

    parametros : any = {}
    flagExibeComentario : boolean = false;
    practicesName : string = "My name is ARNOLD SHWAGSNEGER";

    @Output() enviarInformacaoComentario: EventEmitter<any> = new EventEmitter();
    @Input() practicesInput : any = {};

    constructor() { }

    ngOnInit() {
      this.parametros = this.practicesInput;
      this.parametros.comments = [
        {
          username : "André Uchoa Mesquita",
          comment : "Eu achei essa parada TOP"
        }, 
        {
          username : "Felipe Araujo Munis",
          comment : "Eu achei essa parada TOP 2" 
        }, 
        {
          username : "Mary Top 10",
          comment : "Eu achei essa parada TOP 3" 
        }
      ];
    }

    public abrir(objeto : any){
     
    }

}

