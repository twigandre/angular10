import { Component, OnInit, Injectable } from '@angular/core';
import { ApplicationService } from '../shared/service/aplicationService/application.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html'
})

@Injectable()
export class MembersComponent implements OnInit {

  parametros : any = {};
  exibeGrid : boolean = false;
  servicoMembers = "Members"
  name : string;
  
  constructor(
    private applicationService : ApplicationService,
    private router : Router
  ) { }

  ngOnInit(): void {    
    this.retornaValorSessao();
    if (this.parametros == null ) {
      this.listar();
		} else {
      this.gravarBusca();
    }      
  }

  listar(){
    this.exibeGrid = false;
    this.applicationService.get(this.servicoMembers).subscribe((result: any) => {
      if(result){
        this.parametros = result;
        this.gravarBusca();        
      } else {
        alert("Nenhum usuário encontrado");
      } 
    });
  }

  gravarBusca(){
    sessionStorage.removeItem("members-result");
	  sessionStorage.setItem("members-result", JSON.stringify(this.parametros));
    this.exibeGrid = true;
  }
  
  retornaValorSessao() {	
		if (sessionStorage.getItem("members-result") != null || sessionStorage.getItem("members-result") != "") {
			this.parametros = JSON.parse(sessionStorage.getItem("members-result"));
		}
	}

  buscar(){

    if(!this.name){
      alert("Preencha o nome");
      return false;
    }
   
    var objeto : any = {}
    objeto.name = this.name;

    this.applicationService.get(this.servicoMembers+'/GetMemberName', objeto).subscribe((result: any) => {
      if(result){
        this.parametros = result;
        this.gravarBusca();        
      } else {
        alert("Usuario Não Encontrado");
        return false;
      } 
    });
  }

  limpar(){
    this.name = "";
    this.listar();
  }


}
