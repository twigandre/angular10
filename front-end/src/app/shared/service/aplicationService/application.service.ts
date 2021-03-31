import { Injectable } from '@angular/core';
import { ConverterService } from '../convertService/convert.service';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';

@Injectable() export class ApplicationService {

	token: string;
	authenticated: boolean;

    httpOptions = {
        headers: new HttpHeaders({
        'Content-Type': 'application/json'
        })
    };

	constructor(
		private converterService: ConverterService,
		private http: HttpClient,
        private router: Router
	) { }

	createAuthorizationHeader(): HttpHeaders {
		if (sessionStorage.getItem('token') && !this.token) {
			this.token = sessionStorage.getItem('token');
		}

		if (this.token) {
			const headers = new HttpHeaders({'Authorization':'Bearer ' + this.token});
			sessionStorage.setItem('token', this.token);
			return headers;
		} else {
			return new HttpHeaders();
		}
    }

	removeToken() {
		this.token = null;
		sessionStorage.removeItem('token');
	}

	setToken(token: string, UsuarioInfo: any) {
		this.token = token;
		sessionStorage.setItem('token', this.token);
        sessionStorage.setItem('UsuarioLogado', JSON.stringify(UsuarioInfo));
	}

	get<T>(service: string, data?: any) {

        let url = environment.urlbackend + service;

        if (data) {        
			if (data !== Object(data)) {
				url += '/' + data;
			} else {
				url += '?' + this.converterService.objectToQueryString(data);
            }            
		}

		return this.http.get<T>( url, this.httpOptions );
	}

	post<T>(service: string, data?: any) {
		const url = environment.urlbackend + service;
		return this.http.post<T>(url,JSON.stringify(data),this.httpOptions).pipe();
	}
	
	logout<T>() {       
        if (sessionStorage.getItem('token') != "" && sessionStorage.getItem('token') != undefined){
			sessionStorage.clear();
            this.router.navigate(["/login"]);
        }
	}

	deletar<T>(service: string, data?: any) {
		const url = environment.urlbackend + service + '/' + data;
		return this.http.delete<T>(url, this.httpOptions).pipe();
	}
}

