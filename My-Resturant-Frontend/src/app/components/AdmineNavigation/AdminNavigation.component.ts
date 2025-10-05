import { NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterModule, RouterOutlet } from '@angular/router';
import { Router } from '@angular/router';
import { LogInComponent } from '../log-in/log-in.component';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-AdminNavigation',
  standalone: true,
  imports: [RouterLink,RouterModule,NgIf],
  templateUrl: './AdminNavigation.component.html',
  styleUrl: './AdminNavigation.component.css'
})
export class AdmineNavigationComponent implements OnInit {
  constructor(private router:Router,private auth:AuthService) {}
  sentence:string="Email: support@myrestaurant.com";
  show!:Boolean;
   ngOnInit(): void {
    this.router.navigate(['/AdminNavigation/Home']);
}

IsLoggedIn():boolean{
    if(this.auth.GetToken())
        return true;
    else
        return false;
   }

LogOut(){
  this.auth.LogOut();
  this.router.navigate(["/LogIn"]);
}
getToken():boolean{
  if(localStorage.getItem('token')){
    return true;
  }else{
    return false;
  }

}

}
