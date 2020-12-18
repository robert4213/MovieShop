import { Injectable } from '@angular/core';
import { Observable,BehaviorSubject } from 'rxjs';
import { Login } from 'src/app/shared/models/login';
import { ApiService } from './api.service';
import {map} from 'rxjs/operators' 
import { JwtStorageService } from './jwt-storage.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from 'src/app/shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private user: User;

  private currentLoginUserSubject = new BehaviorSubject<User>({} as User);
  public currentLoginUser = this.currentLoginUserSubject.asObservable();

  private isUserAuthSubject = new BehaviorSubject<boolean>(false);
  public isUserAuth = this.isUserAuthSubject.asObservable();

  constructor(private apiService:ApiService, private jwtStorageService:JwtStorageService) { }

  // login component will call this one
  login(userLogin: Login): Observable<boolean> {
    return this.apiService.create('account/login', userLogin).pipe(
      map((response) => {
        if (response) {
          // once we get the JWT token from API,  Angular will save that token in local storage
          this.jwtStorageService.saveToken(response.token);
          // then decode that token and fill up User object
          // this.decodeJWT();
          this.populateLoginUserInfo();
          return true;
        }
        return false;
      })
    );
  }

  register(){

  }
  // from header component when we click on logout it will call it
  logout(){
    // remove toke from local storage
    this.jwtStorageService.destoryToken();
    // set cuurent user subject to empty object
    this.currentLoginUserSubject.next({} as User);
    // set Auth User subject to false
    this.isUserAuthSubject.next(false);
  }

  private decodeJWT(): User | null {
    // first get the token from local storage
    const token = this.jwtStorageService.getToken();
    // we need to check token is not null and check the token is not expired
    if (!token || new JwtHelperService().isTokenExpired(token)) {
      return null;
    }
    // decode the token and create the User Object
    const decodedToken = new JwtHelperService().decodeToken(token);
    console.log('Decode Token');
    console.log(decodedToken);    
    this.user = decodedToken as User;
    console.log('User');
    console.log(this.user); 
    return this.user;
  }

  public populateLoginUserInfo(){
    if (this.jwtStorageService.getToken()) {
      // const token = this.jwtStorageService.getToken();
      const decodeToken = this.decodeJWT();
      this.currentLoginUserSubject.next(decodeToken);
      this.isUserAuthSubject.next(true);
    }
  }
}
