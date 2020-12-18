import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/shared/models/user';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  isUserAuth:boolean;
  loginUser:User;
  constructor(private authService:AuthenticationService,private router:Router) { }

  ngOnInit(): void {
    this.authService.isUserAuth.subscribe(
      isLogin => {
        this.isUserAuth = isLogin;
        if (this.isUserAuth){
          this.authService.currentLoginUser.subscribe(
            user=>this.loginUser = user
          )
        }
      })
  }

  logout(): void{
    this.authService.logout();
    this.router.navigate(['/login']);
  }

}
