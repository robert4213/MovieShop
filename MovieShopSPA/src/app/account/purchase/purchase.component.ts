import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AccountService } from 'src/app/core/services/account.service';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { Purchase } from 'src/app/shared/models/purchase';

@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.component.html',
  styleUrls: ['./purchase.component.css']
})
export class PurchaseComponent implements OnInit,OnDestroy {
  purchase:Purchase;
  test:string
  subcriptions: Subscription[] = [];
  constructor(private accountService:AccountService, private auth:AuthenticationService,private router:Router) { }

  ngOnInit(): void {
    this.subcriptions.push(this.auth.isUserAuth.subscribe(isLogin=>{
      if(!isLogin){
        this.router.navigate([`/login`])
      }else{
        this.subcriptions.push(this.auth.currentLoginUser.subscribe(user=>{
          this.accountService.getPurchase(user.nameid).subscribe(purchase => {
            this.purchase = purchase;
            console.log(this.purchase);            
          });
        }));
      }
    }));    
  }
  ngOnDestroy(): void {
    this.subcriptions.forEach(sub => {
      sub.unsubscribe();
    });
    
  }

}
