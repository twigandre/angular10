import { Component, OnInit, Output, Input } from '@angular/core';
import { NgxPaginationModule } from 'ngx-pagination';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'grid-members',
  templateUrl: './grid-members.component.html'
})

export class GridMembersComponent implements OnInit {

  jsonMember: any = {};
  
  //pagination
  page:Number = 1;
  sizePagination : Number = 5;
  totalRecords: string;
  id : any;
  idUsuarioLogado : any;
  objetoUsuarioLogado : any = {};
  path: string;
  IconUser = '../../../assets/img/profile-default.png';   

  @Input() parametros: any = {};


  constructor(
    private route: ActivatedRoute,
    private navigate: Router	
  ) 
  { 
    this.path = this.route.snapshot.url[this.route.snapshot.url.length - 1].path;	
    this.objetoUsuarioLogado = JSON.parse(sessionStorage.getItem("UsuarioLogado"));
    this.idUsuarioLogado = Number(this.objetoUsuarioLogado.id);
  }

  ngOnInit(): void {
  }

  spinnerClick1($event){
	  this.sizePagination = Number($event.toElement.value);
	}

  navigateView(item){
    this.navigate.navigate(["/sepractices4ml/practices/"+item.id+"/view"]);
  }

  routeNavigate(item){

    var rota = ""; 
    
    if(item.id != this.idUsuarioLogado){
      rota = "/sepractices4ml/members/"+item.id+"/view";
    } else{
      rota = "/sepractices4ml/members/"+item.id+"/edit"
    }
    
    this.navigate.navigate([rota]);    
  }


}
