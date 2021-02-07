import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ObjectUnsubscribedError } from 'rxjs';
import { Criptografia } from '../../utils/criptografia/criptografia';

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
      private httpClient: HttpClient
    ) { }

  public logar(loginSenha: any) {

      var criptografia = new Criptografia();

      return this.httpClient.post(environment.urlbackend+"Login", JSON.stringify(criptografia.criptografarInputs(loginSenha)), this.httpOptions).pipe();
  }

}
