import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Genre } from "../../shared/models/genre";
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GenreService {

  constructor(private apiService:ApiService) { }

  getAllGenres(): Observable<Genre[]>{
    return this.apiService.getOne('genres');    
  }
}
