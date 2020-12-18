import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Purchase } from 'src/app/shared/models/purchase';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private apiService:ApiService) { }

  getPurchase(id:number):Observable<Purchase>{
    return this.apiService.getOne(`User/${id}/purchases`);
  }

  register(user: any):Observable<any>{
    return this.apiService.create(`Account`,user);
  }
}
