import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MoviesComponent } from '../app/movies/movies.component';
import { PurchaseComponent } from './account/purchase/purchase.component';
import { LoginComponent } from './auth/login/login.component';
import { SignUpComponent } from './auth/sign-up/sign-up.component';
import { HomeComponent } from './home/home.component';
import { MovieDetailsComponent } from './movies/movie-details/movie-details.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'movie/genres/:id',
    component: MoviesComponent,
  },
  {
    path: 'movies/:id',
    component: MovieDetailsComponent
  },
  {
    path:'login',
    component:LoginComponent
  },
  {
    path:'register',
    component:SignUpComponent
  },
  {
    path:'purchase/movie',
    component:PurchaseComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
