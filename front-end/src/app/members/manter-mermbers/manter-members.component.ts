import { Component, OnInit, Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationService } from '../../shared/service/aplicationService/application.service';
import { MembersVM } from '../../shared/view-models/MembersVM';
import { EnumRestResponse } from '../../shared/enums/EnumRestResponse';

@Component({
  selector: 'app-manter-members',
  templateUrl: './manter-members.component.html'
})

@Injectable()
export class ManterMembersComponent implements OnInit {

  parametros : any = {};
  jsonMember: any = {};
  path : string;
  servicoMembers = "Members"
  disableName : boolean = false;
  disableEmail : boolean = false;
  idUsuario : number;
  exibirImagem : boolean = false;

  constructor(
    private applicationService : ApplicationService,
    private route: ActivatedRoute,
    private router: Router,
  ) { 

    this.path = this.route.snapshot.url[this.route.snapshot.url.length - 1].path;	
    this.idUsuario = Number(this.route.snapshot.paramMap.get("idUser"));

    if(this.path != 'new'){
      this.disableEmail = true;
      this.disableName = true;
    }
  }

  ngOnInit(): void {
    this.buscar();       
  }

  buscar(){
    this.applicationService.get<MembersVM>(this.servicoMembers, this.idUsuario).subscribe((result: MembersVM) => {
      if(result){
        this.parametros = result;
        this.exibirImagem = true;
      } else {
        alert("Usuário não encontrado, verifique login e senha e tente novamente.")
      } 
    });
  }

  validar(){
    if(!this.parametros.degree){
      alert("Campo Obrigatório não informado: 'Degree'");
    }
    if(!this.parametros.name){
      alert("Campo Obrigatório não informado: 'Name'");
    }
    if(!this.parametros.email){
      alert("Campo Obrigatório não informado: 'Email'");
    }
    if(!this.parametros.organization){
      alert("Campo Obrigatório não informado: 'Organization'");
    }
    if(!this.parametros.areaActuationRole){
      alert("Campo Obrigatório não informado: 'Area of actuation/ role'");
    }    
    this.salvar();
  }

  salvar(){
    this.applicationService.post(this.servicoMembers, this.parametros).subscribe((result) => {
      if(result == EnumRestResponse.SUCCESS){
        alert('Operação realizada com Sucesso');
        sessionStorage.removeItem("members-result");
        this.router.navigate(["/sepractices4ml/members"]);
      } else {
        alert("Falha ao executar operação");
        return false;
      } 
    });
  }

  habilitarButtonEdit(){
    this.disableName ? 
      this.disableName = false :
       this.disableName = true;
  }

  habilitarButtonEmail(){
    this.disableEmail ? 
      this.disableEmail = false : 
        this.disableEmail = true;
  }

}
