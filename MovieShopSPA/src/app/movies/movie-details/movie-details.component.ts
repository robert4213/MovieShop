import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MoviesService } from 'src/app/core/services/movies.service';
import { Movie } from 'src/app/shared/models/movie';
import {MovieDetail} from '../../shared/models/movie-detail';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {

  constructor(private route: ActivatedRoute, private movieService: MoviesService) { }
  movieId:number = 0;
  movie: MovieDetail | undefined;

  ngOnInit(): void {
    this.route.paramMap.subscribe(
      p => {
        this.movieId = + p.get('id')!;
        this.movieService.getMovieDetail(this.movieId).subscribe((m) => {
          this.movie = m;
          console.log(this.movie);
        });
      }
    );
  }

}
