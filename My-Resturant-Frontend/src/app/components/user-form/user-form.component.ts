import { Component, OnInit, ViewChild, viewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { userServices } from '../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FaIconComponent, FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faSave, faTimes } from '@fortawesome/free-solid-svg-icons';
import { editUser } from '../../interFaces/User';
@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [FormsModule, CommonModule,FontAwesomeModule],
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css','../shared/shared-css/shared-forms.css']
})
export class UserFormComponent implements OnInit {
  faSave = faSave;
  faTimes = faTimes;
  @ViewChild('userForm') userForm!: NgForm;

  user: editUser = {
    firstName: null,
    lastName: null,
    isActive: null,
    role: null,
    email: null,
    password: null,
    phone: null
  };

  emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  notValidPassword = false;
  notValidEmail = false;
  userId: number | null = null;
  formSubmitted = false;
  hashedEmail: string|null=null;

  constructor(
    private userServices: userServices,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.userId) {
      this.loadUserData(this.userId);
    }
  }

  loadUserData(userId: number): void {
    this.userServices.GetPersonById(userId).subscribe({
      next: (user) => {
        this.user = user;
         this.hashedEmail= user.email;

      },
      error: (err) => {
        console.error('Error loading user data:', err);
      }
    });
  }

  showError(fieldName: string): boolean {
    const field = this.userForm?.controls[fieldName];
    return (this.formSubmitted && field?.invalid) || false;
  }

  handleSubmit(): void {
    this.formSubmitted = true;
    this.notValidEmail = false;
    this.notValidPassword = false;

    if(this.userId && this.user.email===this.hashedEmail){
      this.user.email="emailNotChanged";
    }
    else{

        if (!this.emailPattern.test(this.user.email??"")) {
          this.notValidEmail = true;
          return;
        }
      }

    if (!this.userId && (!this.user.password || this.user.password.length < 6)) {
      this.notValidPassword = true;
      return;
    }

    if (this.userForm.valid) {
      if (this.userId) {
        this.updateUser();
      } else {
        this.addUser();
      }
    }
  }

  addUser(): void {
    this.userServices.AddUser(this.user).subscribe({
      next: (response) => {
        this.router.navigate(['/AdminNavigation/Users']);
      },
      error: (error) => {
        console.error('Error adding user:', error);
      }
    });
  }

  updateUser(): void {
    this.userServices.UpdatePerson(this.user, this.userId!).subscribe({
      next: (response) => {
        this.router.navigate(['/AdminNavigation/Users']);
      },
      error: (error) => {
        console.error('Error updating user:', error);
      }
    });
  }

  cancel(): void {
    this.router.navigate(['/AdminNavigation/Users']);
  }
}


