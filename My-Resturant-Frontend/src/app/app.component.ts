import { Component } from '@angular/core';
import { ROUTER_CONFIGURATION, RouterLink, RouterModule, RouterOutlet } from '@angular/router';
import {config} from './app.config.server';
import { AdmineNavigationComponent } from "./components/AdmineNavigation/AdminNavigation.component";
import { UsersComponent } from './components/users/users.component';
import { LogInComponent } from "./components/log-in/log-in.component";


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'my-app';
  color!:string;
}
