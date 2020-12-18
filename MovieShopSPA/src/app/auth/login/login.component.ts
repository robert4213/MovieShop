import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { Login } from 'src/app/shared/models/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  userLogin:Login = {
    email:'',
    password:''
  };
  invalidLogin:boolean = false;
  returnUrl:string;
  
  constructor(private authService:AuthenticationService,private router:Router,private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(param=>this.returnUrl = param.returnUrl||'/');
  }

  login(){   
    this.authService.login(this.userLogin).subscribe(
      response=>{
        if(response){
          this.invalidLogin = false;
          // redirect
          this.router.navigate([this.returnUrl]);
        }else{
          this.invalidLogin = true;
        }
      }, (err:any)=>{
        this.invalidLogin = true;
      }
    )    
  }
}
