import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from "@angular/common/http";
import { environment } from 'src/environments/environment';
import {map} from 'rxjs/operators' 
import { Observable } from 'rxjs';
import { JwtStorageService } from './jwt-storage.service';
import { AuthenticationService } from './authentication.service';

@Injectable({
	providedIn: 'root'
})
export class ApiService {
	private headers:HttpHeaders;


	constructor(protected http: HttpClient, private jwtService:JwtStorageService) {
		this.headers = new HttpHeaders().set('Content-type','application/json');	
		
		// this.authService.isUserAuth.subscribe(isAuth=>{
		// 	if(isAuth){
		// 		this.headers.set('Authorization',  `Bearer ${jwtService.getToken()}`);
		// 	}else{
		// 		this.headers.set('Authorization',  ``);
		// 	}
		// })
	 }
	getAll(path: string):Observable<any[]> {
		this.updateToke();
		return this.http.get(`${environment.apiUrl}${path}`,{headers : this.updateToke()})
		.pipe(map(resp=> resp as any[]));
	}

	getOne(path:string, id?:number):Observable<any>{
		let getUrl:string;
		this.updateToke();
		if(id){
			getUrl = `${environment.apiUrl}${path}/${id}`;
		}else{
			getUrl = `${environment.apiUrl}${path}`;
		}
		console.log(this.headers);		
		return this.http.get(getUrl,{headers : this.updateToke()}).pipe(map(resp=>resp as any));
	}

	create(path:string, resource:any, options?:any):Observable<any>{
		this.updateToke();
		return this.http.post(`${environment.apiUrl}${path}`,resource,{headers : this.updateToke()})
		.pipe(map(response => response as any));
	}

	private updateToke():HttpHeaders{
		if(this.jwtService.getToken()) return new HttpHeaders().set('Content-type','application/json').set('Authorization',  `Bearer ${this.jwtService.getToken()}`);
		else return new HttpHeaders().set('Content-type','application/json');
	}
}
