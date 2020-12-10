export interface MovieDetail {
  id: number;
  title: string;
  posterUrl: string;
  backdropUrl: string;
  rating: number;
  overview: string;
  tagline: string;
  budget: number;
  revenue: number;
  imdbUrl: string;
  tmdbUrl: string;
  releaseDate: string;
  runTime: number;
  price: number;
  favoritesCount: number;
  casts: Cast[];
  genres: Genre[];
}

interface Genre {
  id: number;
  name: string;
}

interface Cast {
  id: number;
  name: string;
  gender: string;
  tmdbUrl: string;
  profilePath: string;
  character: string;
}
