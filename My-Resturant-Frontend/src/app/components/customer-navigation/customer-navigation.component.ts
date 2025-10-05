import { Component, OnInit } from '@angular/core';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-customer-navigation',
  standalone: true,
  imports: [RouterOutlet, RouterModule],
  templateUrl: './customer-navigation.component.html',
  styleUrl: './customer-navigation.component.css'
})
export class CustomerNavigationComponent implements OnInit{
  constructor(public router: Router, public auth: AuthService){}
  theSentence: string = "Email: support@myrestaurant.com";

  ngOnInit(): void {
    this.router.navigate(['/CustomerNavigation/Home']);
  }

  LogOut(){
    this.auth.LogOut();
    this.router.navigate(["/LogIn"]);
  }
}
