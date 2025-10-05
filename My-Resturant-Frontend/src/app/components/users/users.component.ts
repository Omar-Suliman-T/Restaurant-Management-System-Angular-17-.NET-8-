import { Component, OnInit } from '@angular/core';
import { user } from '../../interFaces/User';
import { userServices } from '../../services/user.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ConfirmDialogComponent } from "../shared/confirm-dialog/confirm-dialog.component";

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [CommonModule, RouterModule, ConfirmDialogComponent],
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  constructor(private userServices: userServices) {}

  users: user[] = [];
  showConfirm: boolean = false;
  userId: number = 0;

  ngOnInit(): void {
    this.loadUsers();
  }

   loadUsers(): void {
    this.userServices.getUsers().subscribe({
      next: (theUsers) => {
        console.log('Raw users data:', theUsers);
        this.users = theUsers;
        console.log('Processed users:', this.users);
      },
      error: (err) => {
        console.error('Error loading users:', err);
      }
    });
  }

  deletePerson(id: number): void {
    this.userServices.DeletePerson(id).subscribe({
      next: (response) => {
        this.users = this.users.filter(user => user.id !== id);
      },
      error: (err) => {
        console.error('Error deleting user:', err);
      }
    });
  }

  onConfirm(confirmed: boolean): void {
    this.showConfirm = false;
    if (confirmed) {
      this.deletePerson(this.userId);
    }
  }
}
