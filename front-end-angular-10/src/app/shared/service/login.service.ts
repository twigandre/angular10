import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ObjectUnsubscribedError } from 'rxjs';
import { CriptografiaComponent } from '../../components/criptografia/criptografia.component';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(
      private httpClient: HttpClient,
      private CriptografiaComponent : CriptografiaComponent
      ) { }

  public logar(loginSenha: any) {

      //var objetoLogin = this.CriptografiaComponent.criptografar(loginSenha);

      return this.httpClient.post(environment.urlbackend+"Login", JSON.stringify(loginSenha), this.httpOptions).pipe();
  }

}
