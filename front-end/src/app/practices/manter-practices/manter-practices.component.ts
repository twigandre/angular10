import { Component, OnInit, Injectable, ViewChild, Output, EventEmitter, ElementRef, Input, Renderer2  } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from "@angular/forms";
import { connectableObservableDescriptor } from 'rxjs/internal/observable/ConnectableObservable';
import * as $ from 'jquery';
import { ActivatedRoute, Router } from '@angular/router';
import { ComentPracticesComponent } from '../comentarios/comment-practices.component';
import { ApplicationService } from '../../shared/service/aplicationService/application.service';
import { PracticesAnexoVM } from '../../shared/view-models/PracticesAnexoVM';
import { MembersVM } from '../../shared/view-models/MembersVM';
import { PracticesVM } from '../../shared/view-models/PracticesVM';
import { PracticesAuthorsVM } from '../../shared/view-models/PracticesAuthorsVM';

@Component({
  selector: 'app-manter-practices',
  templateUrl: './manter-practices.component.html'
})
export class ManterPracticesComponent implements OnInit {

  parametros = new PracticesVM();
  indiceAuthor : number = 0;
  idPractice : number = 0;
  paginaSelecionada : number = 1;

  idAnalistaSelecionado : number;

  objetoAuthorCombobox : MembersVM[];

  objetoPracticesAuthors = new Array<PracticesAuthorsVM>();
  objetoAutoresApagados : PracticesAuthorsVM[];

  objetoAnexos = new Array<PracticesAnexoVM>();
  flagExisteInputVazio : boolean = false;
  flagTemAnexo: boolean = false;
  flagTerminouConsultaAuthors: boolean = false;
  exibirComentarios: boolean = false;

  //--flag que recupera a rota
  path : string;

  //--controle anexar pdf---
	limiteArquivo = 10485760; // 10MB
	temArquivo = false;
	filetype: string;
	filesize: number;
  filename: string;

  objetoUsuarioLogado : any = {};
  idUsuarioLogado : number;
	
  typePdf = ['application/pdf'];
  typeImage =  ['image/jpg', 'image/png', 'image/gif', 'image/jpeg'];

  servicePractices = "Practices"
  servicePracticesAuthors = "PracticesAuthors";
  servicePracticesAnexo = "PracticesAnexo";

  //--array authors--
  authors : any = ['Jose3' , 'Pedro3', 'Mario3', 'Felipe3', 'Andre3', 'Mary Key3'];

  @ViewChild('div') div: ElementRef;
  @ViewChild('delete') delete: ElementRef;
  @ViewChild('CommentsComponent') CommentsComponent: ElementRef;;
  
  private ComentPracticesComponent : ComentPracticesComponent

  constructor(
    private renderer: Renderer2,
    private applicationService : ApplicationService,
    private route: ActivatedRoute,
	  private router: Router 
  ) 
  {     
    this.path = this.route.snapshot.url[this.route.snapshot.url.length - 1].path;
    this.idPractice = Number(this.route.snapshot.paramMap.get("idPractice"));
    this.objetoUsuarioLogado = JSON.parse(sessionStorage.getItem("UsuarioLogado"));
    this.idUsuarioLogado = Number(this.objetoUsuarioLogado.id);	
  }

  ngOnInit(): void {
    this.preencherComboAutors();
    (this.path != 'new') ? this.buscar() : '';
  }

  buscar(){
    this.applicationService.get(this.servicePractices, this.idPractice).subscribe((result : PracticesVM) => {
      if(result){

        if(result.anexosPractice.length > 0){
          this.objetoAnexos = result.anexosPractice;
          this.flagTemAnexo = true;
        }        

        if(result.authorsPractice.length > 0){
          this.objetoPracticesAuthors = result.authorsPractice;
        }

        this.parametros = result;

      } else {
        alert("Falha ao buscar autor");
        return false;
      }
    });    
  }

  preencherComboAutors(){
    this.applicationService.get(this.servicePractices+"/GetAuthors").subscribe((result : MembersVM[]) => {
      if(result){
        this.objetoAuthorCombobox = result;
        this.flagTerminouConsultaAuthors = true;
      } else {
        alert("Falha ao listar combo de autores");
        return false;
      } 
    });
  }

  onFileChange(event) {

		let reader = new FileReader();

		if (event.target.files && event.target.files.length > 0) {
			
      let file = event.target.files[0];
			reader.readAsDataURL(file);

      this.filetype = file.type;
			this.filesize = file.size;
      this.filename = file.name;

			if(this.typePdf.includes(this.filetype) || this.typeImage.includes(this.filetype)) {
				if(this.filesize < this.limiteArquivo) {
					this.temArquivo = true;

					reader.onload = () => {

						var objectPdf = (reader.result as string).split(',')[1];

            let objetoParametro : any = {};
            objetoParametro = {  
              extensaoAnexo : this.filetype,
              objetoAnexo : objectPdf,
              name : this.filename,
              idPractice : this.idPractice ? this.idPractice : 0
            };
           
            this.objetoAnexos.push(objetoParametro);
            this.flagTemAnexo = true;
					};
				} else {
				alert('Tamanho nao pode ser superior a 10 MB');
        return false;
				}
			} else {
				alert('Apenas Pdf ou Imagem');
        return false;
			}
		} else {
			this.temArquivo = false;
		}
	}

  spinnerClick($event){
   
    if(this.idAnalistaSelecionado != undefined && this.idAnalistaSelecionado != null){     
      var idUser = Number(this.idAnalistaSelecionado);
      let arrayAuthors :  MembersVM[];
      arrayAuthors = this.objetoAuthorCombobox.filter(x => x.idUsuario == idUser);
      var authorSelecionado = arrayAuthors[0];

      let objectPractices : any = Array();
      
      objectPractices = [{      
        idPractice : (this.idPractice > 0) ? this.idPractice : 0,
        idUser : authorSelecionado.idUsuario,
        name: authorSelecionado.name,
        userImage : authorSelecionado.imagemMembro      
      }]
            
      if(!this.objetoPracticesAuthors)
      {       
        this.objetoPracticesAuthors = [].concat(objectPractices);      
      }
      else
      {
        var authorJaExisteNoArray = this.objetoPracticesAuthors.filter(x => x.idUser == idUser);
        if(authorJaExisteNoArray.length == 0){         
          this.objetoPracticesAuthors = this.objetoPracticesAuthors.concat(objectPractices);
          console.log(this.objetoPracticesAuthors);        
        }
      }      

    }

	}

  validar(){

    if(!this.parametros.name){
      alert('Campo Obrigatório não informado: Name');
      this.paginaSelecionada = 1;
      return false;
    }

    if(!this.parametros.description){
      alert('Campo Obrigatório não informado: Description');
      this.paginaSelecionada = 1;
      return false;
    }  

    if(!this.objetoPracticesAuthors.length){
      alert('Campo Obrigatório não informado: Authors');
      this.paginaSelecionada = 1;
      return false;
    } else{
      this.parametros.authorsPractice = this.objetoPracticesAuthors;
    }

    if(!this.parametros.typesAiMlApplications){
      alert('Campo Obrigatório não informado: Types os AI/ML applications');
      this.paginaSelecionada = 1;
      return false;
    } 

    if(!this.parametros.organizationContext){
      alert('Campo Obrigatório não informado: Organization Context');
      this.paginaSelecionada = 1;
      return false;
    } 

    if(!this.parametros.seKnowLedge){
      alert('Campo Obrigatório não informado: SE Knowledge Areas');
      this.paginaSelecionada = 2;
      return false;
    } 

    if(!this.parametros.contribuitionTypes){
      alert('Campo Obrigatório não informado: Contribuition Types');
      this.paginaSelecionada = 2;
      return false;
    } 

    if(!this.objetoAnexos.length){
      alert('Campo Obrigatório não informado: Files');
      this.paginaSelecionada = 2;
      return false;
    } else {
      this.parametros.anexosPractice = this.objetoAnexos; 
    }

    if(!this.parametros.link){
      alert('Campo Obrigatório não informado: Link');
      this.paginaSelecionada = 2;
      return false;
    } 

    if(!this.parametros.referencesDescribing){
      alert('Campo Obrigatório não informado: References describing the Practice');
      this.paginaSelecionada = 2;
      return false;
    }

    this.parametros.idUser = this.idUsuarioLogado;

    this.salvar();
  }

  salvar(){
    this.applicationService.post(this.servicePractices, this.parametros).subscribe((result : any) => {
      if(result){
        alert("success!");
        var rota: string = "/sepractices4ml/practices";
        this.router.navigate([rota]);
      }
    });
  }

  baixarAnexo(item) {
		if (item) {
			const hashPDF = item.objetoAnexo;
			const linkSource = 'data:' + 'application/pdf' + ';base64,' + hashPDF;
			const downloadLink = document.createElement('a');
			const fileName = item.name;      
			document.body.appendChild(downloadLink);
			downloadLink.href = linkSource;
			downloadLink.download = fileName;
			downloadLink.target = '_self';
			downloadLink.click();
		} else {
			alert('Erro ao baixar arquivo!');
      return false;
		}
	}

  RemoverAnexo(objeto : PracticesAnexoVM){		

    var index = 0;
    
    var option = confirm("Deseja apagar o arquivo?");

    if(option){

        if(this.objetoAnexos.length == 1 && this.path=='edit'){
          alert('A practice nao pode ficar sem anexo!');
          return false;
        }

        if(objeto.id > 0){ //se for um anexo que ja esta salvo na base, deverá apagar no back-end.
          this.ApagarAnexoBackend(objeto);
        }      

        for (const iterator of this.objetoAnexos) {
          if(iterator.name == objeto.name){
            this.objetoAnexos.splice(index, 1); 
  
            (this.objetoAnexos.length == 0 || !this.objetoAnexos.length) ?
              this.flagTemAnexo = false : '';
  
            break;
          } 
          index++;
        }      
    }
		
	}

  exibirComentario(){
    if(this.exibirComentarios){
      this.exibirComentarios = false;
    } else{
      this.exibirComentarios = true;
      window.location.hash = '#comments';
    }   
  }   
  
  OutputInfoComents($event){
    if($event === 'fechar formulario'){
      this.exibirComentarios = false;
    }
  }

  removerAutor(item : PracticesAuthorsVM)
  {  

    var option = confirm("Deseja Remover o Autor?");

    if(option){
           
      if(this.objetoPracticesAuthors.length == 1 && this.path == 'edit'){
        alert('A practice nao pode ficar sem autor!');
        return false;
      }       
      
      if(item.id > 0){ //se for um autor que ja esta salvo na base, deverá apagar no back-end
        this.ApagarAutorBackend(item);
        return false;
      } 

      this.objetoPracticesAuthors.forEach(element => {   
        item.idUser == element.idUser ?
          this.objetoPracticesAuthors.splice(this.objetoPracticesAuthors.indexOf(element), 1) : 
            '';              
      });   
    }        
  }

  ApagarAutorBackend(objeto : PracticesAuthorsVM){   
      this.applicationService.deletar(this.servicePracticesAuthors, Number(objeto.id)).subscribe((result : boolean) => {        
        if(result){   
          alert("Autor removido");      
          this.buscar();        
        } else {         
          alert("Falha ao apagar autor.");
          return false;
        } 
      });    
  }

  ApagarAnexoBackend(objeto : PracticesAnexoVM){
    var option = confirm("Deseja Remover o Autor?");
    if(option){
      this.applicationService.deletar(this.servicePracticesAnexo, Number(objeto.id)).subscribe((result : boolean) => {        
        if(result){   
          alert("Anexo removido");      
          this.preencherComboAutors();        
        } else {         
          alert("Falha ao apagar anexo.");
          return false;
        } 
      });
    }    
  }

  selectPaged(number){
    this.paginaSelecionada = number;
  }

}