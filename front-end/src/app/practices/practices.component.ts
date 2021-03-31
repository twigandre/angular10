import { Component, OnInit } from '@angular/core';
import { ApplicationService } from '../shared/service/aplicationService/application.service';
import { PracticesVM } from '../shared/view-models/PracticesVM';


@Component({
  selector: 'app-practices',
  templateUrl: './practices.component.html',
  styleUrls: ['./practices.component.css']
})
export class PracticesComponent implements OnInit {

  parametros : any = {};
  arrayPractices : PracticesVM[];
  exibirGrid : boolean = false;
  servico = "Practices";
  resultadoSessionStorage : any = {};

  constructor(private applicationService : ApplicationService) { }

  ngOnInit(): void {
    this.retornaValorSessao();
    if (this.resultadoSessionStorage == null ) {
      this.buscar('listar');
		} else {
      this.gravarBusca();
      this.exibirGrid = true;
    }     
  }

  novo(){}

  buscar(action : string){

    if(action == 'buscar'){
      if(!this.parametros.name)
        alert('Preecha o nome da Practice')
    }

    this.applicationService.get(this.servico, this.parametros).subscribe((result : PracticesVM[]) => {        
      if(result){           
        this.arrayPractices = result;
        this.exibirGrid = true;        
        this.gravarBusca();       
      } else {            
        alert("Practices não encontrada.");
        this.arrayPractices = null;
        this.exibirGrid = false;
        return false;
      } 
    });
  }

  gravarBusca(){
    sessionStorage.removeItem("practices-result");
	  sessionStorage.setItem("practices-result", JSON.stringify(this.arrayPractices));
  }
  
  retornaValorSessao() {	
    this.resultadoSessionStorage = sessionStorage.getItem("practices-result");
		if (this.resultadoSessionStorage != null && this.resultadoSessionStorage != "") {
			this.arrayPractices = JSON.parse(this.resultadoSessionStorage);
		}
	}

  limpar(){
      this.parametros = {};
      this.buscar('listar');
  }

}
