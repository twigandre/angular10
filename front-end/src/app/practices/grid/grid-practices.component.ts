import { Component, OnInit, Input } from '@angular/core';
import { NgxPaginationModule } from 'ngx-pagination';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationService } from '../../shared/service/aplicationService/application.service';
import { PracticesComponent } from '../practices.component';

@Component({
  selector: 'grid-practices',
  templateUrl: './grid-practices.component.html'
})

export class GridPracticesComponent implements OnInit {

  jsonPractices: any = {};
  
  //pagination
  page:Number = 1;
  sizePagination : Number = 5;
  totalRecords: string;
  id : any;
  path: string;
  servico = "Practices";

  @Input() parametros: any = {};
  
  objetoUsuarioLogado : any = {};
  idUsuarioLogado : any;

  constructor(
    private practicesComponent : PracticesComponent,
    private applicationService : ApplicationService,
    private route: ActivatedRoute,
    private navigate: Router	
  ) 
  { 
    this.objetoUsuarioLogado = JSON.parse(sessionStorage.getItem("UsuarioLogado"));
    this.idUsuarioLogado = Number(this.objetoUsuarioLogado.id);
  }

  ngOnInit(): void {
    this.populaJson();
  }

  populaJson(){
  }
  
  spinnerClick1($event){
	  this.sizePagination = Number($event.toElement.value);
	}

  navigateView(item){
    this.navigate.navigate(["/sepractices4ml/practices/"+item.id+"/view"]);
  }

  deletePractice(item){

    var option = confirm("Deseja Apagar a Practices?");

    if(option){
      this.applicationService.deletar(this.servico, Number(item.id)).subscribe((result : boolean) => {        
        if(result){ 
          this.practicesComponent.buscar('listar');
        } else {         
          alert("Falha ao apagar practice.");
          return false;
        } 
      });
    }    
  }

}
