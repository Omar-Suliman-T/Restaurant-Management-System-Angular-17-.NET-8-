import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { editUser, signUpUser } from '../../interFaces/User';
import { userServices } from '../../services/user.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [FormsModule, NgIf, RouterModule],
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent {
  user: signUpUser = {
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    phone: ''
  };

  repeatedPassword: string = '';
  isLoading: boolean = false;
  errorMessage: string = '';
  successMessage: string = '';

  constructor(
    private userServices: userServices,
    private authServices: AuthService,
    private router: Router
  ) {}

  validateForm(): boolean {
    // Reset messages
    this.errorMessage = '';

    // Required fields validation
    if (!this.user.firstName || !this.user.lastName || !this.user.email ||
        !this.user.password || !this.user.phone) {
      this.errorMessage = 'All fields are required';
      return false;
    }

    // Email validation
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailPattern.test(this.user.email)) {
      this.errorMessage = 'Please enter a valid email address';
      return false;
    }

    // Password validation
    if (this.user.password.length < 6) {
      this.errorMessage = 'Password must be at least 6 characters long';
      return false;
    }

    // Password match validation
    if (this.user.password !== this.repeatedPassword) {
      this.errorMessage = 'Passwords do not match';
      return false;
    }

    return true;
  }

  onSubmit() {
    if (!this.validateForm()) return;

    this.isLoading = true;
    this.errorMessage = '';

    this.userServices.AddCustomer(this.user).subscribe({
      next: () => {
        this.successMessage = 'Account created successfully! Logging you in...';
        this.authServices.LogIn(this.user.email, this.user.password).subscribe({
          next: (token) => {
            localStorage.setItem('token', token);
            this.router.navigate(['/CustomerNavigation']);
          },
          error: (error) => {
            this.isLoading = false;
            this.errorMessage = 'Login failed. Please try logging in manually.';
            console.error(error);
          }
        });
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage = error.error?.message || 'Registration failed. Please try again.';
        console.error(error);
      }
    });
  }
}
