import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validator, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/core/services/account.service';


@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  user = new FormGroup({
    email:new FormControl('',Validators.email),
    password:new FormControl('',Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&,./]).{8,}$')),
    comfirmPassword:new FormControl('',Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&,./]).{8,}$')),
    firstName:new FormControl('',Validators.maxLength(128)),
    lastName:new FormControl('',[Validators.maxLength(128)]),
  },{validators: this.checkPasswords});
  registerFailed:boolean = false;

  constructor(private accountService:AccountService, private router:Router) { }

  get email() {return this.user.get('email');}
  get password() {return this.user.get('password');}
  get comfirmPassword() {return this.user.get('comfirmPassword');}
  get firstName() {return this.user.get('firstName');}
  get lastName() {return this.user.get('lastName');}

  ngOnInit(): void {
  }

  register(): void{
    this.registerFailed =false;

    if(!this.user.invalid){      
      this.accountService.register(this.user.getRawValue()).subscribe(res=>{
        this.router.navigate([`/`]); 
        console.log(res);
                
      },err=>{
        this.registerFailed =true;
        console.warn(err);
      })
    } 
  }

  checkPasswords(group: FormGroup) { // here we have the 'passwords' group
  let pass = group.get('password').value;
  let confirmPass = group.get('comfirmPassword').value;
  
  return pass === confirmPass ? null : { notSame: true }     
}

}
