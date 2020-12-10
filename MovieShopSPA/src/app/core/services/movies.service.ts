import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Movie } from 'src/app/shared/models/movie';
import { ApiService } from './api.service';
import {MovieDetail} from '../../shared/models/movie-detail';

@Injectable({
  providedIn: 'root'
})
export class MoviesService {

  constructor(private apiService: ApiService) { }

  getTopRevenueMovie(): Observable<Movie[]>{
    return this.apiService.getAll('movies/toprevenue');
  }

  getMovieDetail(id: number): Observable<MovieDetail>{
    return this.apiService.getOne('movies', id);
  }

  getMovieByGenres(id: number): Observable<Movie[]>{
    return this.apiService.getAll(`movies/genre/${id}`);
  }
}
