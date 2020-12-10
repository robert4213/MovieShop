import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MoviesService } from '../core/services/movies.service';
import { Movie } from '../shared/models/movie';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent implements OnInit {
  movies:Movie[] | undefined;
  movieId:number = 0;
  constructor(private movieService:MoviesService, private router:ActivatedRoute) { }

  ngOnInit(): void {
    this.router.paramMap.subscribe(p=>{
      this.movieId = +p.get('id')!;
      this.movieService.getMovieByGenres(this.movieId).subscribe(m=>{
        this.movies = m;
      })
    });

    this.movieService.getMovieByGenres
  }

}
