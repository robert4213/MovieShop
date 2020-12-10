import { Component, OnInit } from '@angular/core';
import { MoviesService } from '../core/services/movies.service';
import { Movie } from '../shared/models/movie';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  movies:Movie[] = [];

  constructor(private movieService:MoviesService) { }

  ngOnInit(): void {
    this.movieService.getTopRevenueMovie().subscribe((m) => {
      this.movies = m;
    });
  }

}
