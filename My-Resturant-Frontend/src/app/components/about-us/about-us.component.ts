import { Component, OnInit } from '@angular/core';
import { RouterLink } from "@angular/router";
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-about-us',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './about-us.component.html',
  styleUrl: './about-us.component.css'
})
export class AboutUsComponent implements OnInit {
  constructor(private auth:AuthService){}
  role:string="";
  ngOnInit(): void {
    this.role=this.auth.getRoleFromToken();
  }

}
