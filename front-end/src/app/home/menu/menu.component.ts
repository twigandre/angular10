import { Component, OnInit, Injectable, ViewChild, Output, EventEmitter, ElementRef, Input } from '@angular/core';
import { ApplicationService } from "../../shared/service/aplicationService/application.service";
import { LoaderService } from "../../shared/loader/loader.service";
import { ActivatedRoute, Router } from '@angular/router';
import { UserVM } from 'src/app/shared/view-models/UserVM';


@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})

export class MenuComponent implements OnInit {

  home : boolean = false;
  practices : boolean = false;
  notifications : boolean = false;
  members : boolean = false;
  about : boolean = false;
  profile : boolean = false;
  objetoUsuarioLogado : UserVM;

  qtdNotification: number = 0;

  servicoNotificacao = "Notification";

  constructor( 
    private applicationService : ApplicationService,
    public loaderService : LoaderService,
    private route: ActivatedRoute,
		private router: Router) { }

  ngOnInit(): void {
    this.objetoUsuarioLogado = JSON.parse(sessionStorage.getItem("UsuarioLogado"));
    this.buscarNotificacoes();
  }

  logout(){
   this.applicationService.logout();
  }

  buscarNotificacoes(){

    var objeto = {
      idUsuario : this.objetoUsuarioLogado.id
    }
    this.applicationService.get(this.servicoNotificacao, objeto).subscribe((result: any) => {
      if(result){
        this.qtdNotification = result.length;
        // this.parametros = result;
        // this.gravarBusca();        
      } 
    });

  }

}
