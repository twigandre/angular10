import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private httpClient: HttpClient) { }

  public logar(loginSenha: any) {
      return this.httpClient.post(environment.urlbackend+"Login", JSON.stringify(loginSenha), this.httpOptions).pipe();
  }

}
