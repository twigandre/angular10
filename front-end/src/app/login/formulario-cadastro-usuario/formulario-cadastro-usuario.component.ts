import { Component, OnInit, Injectable, ViewChild, Output, EventEmitter, ElementRef, Input } from '@angular/core';
import { Router } from '@angular/router';
import { DomSanitizer } from "@angular/platform-browser";
import { SocialAuthService,  } from "angularx-social-login";
import { FacebookLoginProvider, GoogleLoginProvider } from "angularx-social-login";
import { SignFacebookVM } from "../../shared/view-models/SignFacebookVM";
import { SignGoogleVM } from "../../shared/view-models/SignGoogleVM";
import { EnumRestResponse } from "../../shared/enums/EnumRestResponse";
import { UserVM } from "../../shared/view-models/UserVM";
import { ApplicationService } from "../../shared/service/aplicationService/application.service";

@Component({
  selector: 'app-cadastro-usuario',
  templateUrl: './formulario-cadastro-usuario.component.html'
})
export class FormularioCadastroUsuarioComponent implements OnInit {

  login : string;
  senha : string;
  parametros = new UserVM;
  statusLogin : number;  
  informacao : string = ""

  //--controle anexar pdf---
  limiteArquivo = 10485760; // 10MB
  temArquivo = false;
  filetype: string;
  filesize: number;
  filename: string;
  types = 'image/';
  flagTemAnexo: boolean = false;
  IconUser = '../../../assets/img/profile-default.png'; 

  isLoginGoogle : boolean = false;
  isLoginFacebook : boolean = false;
  exbirFacebookEGoogle : boolean = false;
  user: any;

  servicoUsuario = "User";

  constructor(
	private applicationService : ApplicationService,
	private authService: SocialAuthService,
	private router: Router,    
	private domSanitizer: DomSanitizer		
    ) { }

  ngOnInit(){
	this.authService.authState.subscribe(user => {

		if(this.isLoginGoogle || this.isLoginFacebook){

			this.isLoginGoogle ? 
			this.user = SignGoogleVM : 
				this.user = SignFacebookVM;

		if(!user.email){
			alert('A rede social nao possui um e-mail! cadastre-se preenchendo todos os campos, e não selecionando uma rede social.');
			this.isLoginGoogle = false;
			this.isLoginFacebook = false;
			return false;
		}
			
			this.IconUser = user.photoUrl;		
			this.user = user;
			this.saveUserWithOtherAutentication(this.user);
		}
		
	});
  }

  saveUserWithOtherAutentication(user){	

	var objetoParametro = new UserVM;
	objetoParametro.userImage = user.photoUrl;
	objetoParametro.userName = user.email;//user.userName;
	objetoParametro.email = user.email;
	objetoParametro.name = user.name;
	objetoParametro.password = (this.isLoginGoogle) ? 'Autentication with Google' : 'Autentication with Facebook';
	
	this.applicationService.post(this.servicoUsuario, objetoParametro).subscribe((result) => {
        if(result === EnumRestResponse.SUCCESS) {
		  alert("Operação realizada com sucesso!");
		  this.voltar();
        } else if(result === EnumRestResponse.ERROR_USUARIO_JA_CADASTRADO_COM_FACEBOOK) {
			alert("Conta do Facebook já Cadastrada!");
		} else if(result === EnumRestResponse.ERROR_USUARIO_JA_CADASTRADO_COM_GOOGLE) {
			alert("Conta Google já Cadastrada!");
		} else if(result === EnumRestResponse.ERROR_EMAIL_JA_CADASTRADO) {
			alert("Email já Cadastrado!");
		} else if(result === EnumRestResponse.ERROR_USERNAME_JA_CADASTRADO) {
			alert("User name já Cadastrado!");
		} else if(result == EnumRestResponse.SERVER_ERROR) {
            alert("Server error!" + " Falha ao salvar usuário.");
        }
	});
	
  }

  validar(){}

  voltar(){
	this.isLoginGoogle = false;
	this.isLoginFacebook = false;
    var rota: string = "/login";
    this.router.navigate([rota]);
  }
  
  onFileChange(event) {

	let reader = new FileReader();

	if (event.target.files && event.target.files.length > 0){
			let file = event.target.files[0];

			reader.readAsDataURL(file);

			this.filetype = file.type;
			this.filesize = file.size;
			this.filename = file.name;

			if(this.filename.length < 50) {
				if (this.filetype.includes(this.types)){
					if (this.filesize < this.limiteArquivo){
						//this.parametros.descricaoAnexo = file.name;

						let descricao = file.name;
						let anexo;
						let anexoRender;

						let that = this;
						reader.onload = () => {
							let who = that;
							reader.onloadend = (e) =>{
							anexoRender = who.domSanitizer.bypassSecurityTrustUrl(reader.result as string);
							this.parametros.userImage = anexoRender.changingThisBreaksApplicationSecurity;						          
               				this.IconUser = anexoRender.changingThisBreaksApplicationSecurity;
						  }
						};

					}else {
					alert('tamanho da imagem muito grande');
					}

				}else {
					alert("Favor selecionar um arquivo no formato de imagem.");
				}
			}else {
				alert("Descrição do arquivo não pode ser superior a 50 caracteres");
			}
		}else{
			this.temArquivo = false;
		}
  }

  exibirRedesSociais(){
    this.exbirFacebookEGoogle = this.exbirFacebookEGoogle ? false : true;
  }

  loginWithGoogle(){
	this.isLoginGoogle = true;
	this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }

  loginWithFacebook(){
	this.isLoginFacebook = true;
	this.authService.signIn(FacebookLoginProvider.PROVIDER_ID);
  }
 
  salvarUsuario(){
	  
	if(this.parametros.password != this.parametros.passwordConfirm){
		alert("Password são diferentes");
		return false;
	}

	this.applicationService.post(this.servicoUsuario, this.parametros).subscribe((result) => {
        if(result === EnumRestResponse.SUCCESS) {
		  alert("Operação realizada com sucesso!");
		  this.voltar();
        } else if(result === EnumRestResponse.ERROR_USUARIO_JA_CADASTRADO_COM_FACEBOOK) {
			alert("Conta do Facebook já Cadastrada!");
		} else if(result === EnumRestResponse.ERROR_USUARIO_JA_CADASTRADO_COM_GOOGLE) {
			alert("Conta Google já Cadastrada!");
		} else if(result === EnumRestResponse.ERROR_EMAIL_JA_CADASTRADO) {
			alert("Email já Cadastrado!");
		} else if(result === EnumRestResponse.ERROR_USERNAME_JA_CADASTRADO) {
			alert("User name já Cadastrado!");
		} else if(result == EnumRestResponse.SERVER_ERROR) {
            alert("Server error!" + " Falha ao salvar usuário.");
        }
	});
  }

  limparImagem(){
	this.parametros.userImage = null;
	this.IconUser = '../../../assets/img/profile-default.png'; 
  }


}
