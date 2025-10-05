import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { trigger, transition, style, animate, query, stagger } from '@angular/animations';

@Component({
  selector: 'app-log-in',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule],
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css'],
  animations: [
    trigger('formSwitch', [
      transition('* <=> *', [
        style({ position: 'relative' }),
        query(':enter, :leave', [
          style({
            position: 'absolute',
            top: 0,
            left: 0,
            width: '100%',
            opacity: 0,
            transform: 'scale(0.95)'
          })
        ], { optional: true }),
        query(':enter', [
          animate('400ms ease-out', style({
            opacity: 1,
            transform: 'scale(1)'
          }))
        ], { optional: true })
      ])
    ]),
    trigger('slideIn', [
      transition(':enter', [
        style({
          opacity: 0,
          transform: 'translateY(20px)'
        }),
        animate('300ms cubic-bezier(0.4, 0, 0.2, 1)', style({
          opacity: 1,
          transform: 'translateY(0)'
        }))
      ])
    ]),
    trigger('fadeIn', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('200ms ease-out', style({ opacity: 1 }))
      ]),
      transition(':leave', [
        animate('150ms ease-in', style({ opacity: 0 }))
      ])
    ]),
    trigger('expandIn', [
      transition(':enter', [
        style({
          opacity: 0,
          height: '0',
          marginBottom: '0',
          transform: 'translateY(-10px)'
        }),
        animate('350ms cubic-bezier(0.4, 0, 0.2, 1)', style({
          opacity: 1,
          height: '*',
          marginBottom: '*',
          transform: 'translateY(0)'
        }))
      ])
    ]),
    trigger('stagger', [
      transition(':enter', [
        query('.form-group', [
          style({ opacity: 0, transform: 'translateX(-20px)' }),
          stagger('100ms', [
            animate('300ms ease-out', style({
              opacity: 1,
              transform: 'translateX(0)'
            }))
          ])
        ], { optional: true })
      ])
    ])
  ]
})
export class LogInComponent {
  email: string = '';
  password: string = '';
  loading: boolean = false;
  errorMessage: string = '';
  showPassword: boolean = false;
  rememberMe: boolean = false;
  showResetPasswordForm: boolean = false;
  code: string = "";
  message: string = "";
  codeSent: boolean = false;

  constructor(
    private router: Router,
    private auth: AuthService
  ) {}

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  showResetPassword(): void {
    this.showResetPasswordForm = true;
    this.errorMessage = '';
    this.message = '';
  }

  backToLogin(): void {
    this.showResetPasswordForm = false;
    this.codeSent = false;
    this.code = '';
    this.message = '';
  }

  login(): void {
    if (this.loading) return;

    console.log('Login attempt with:', {
      email: this.email,
      password: this.password,
      rememberMe: this.rememberMe
    });

    this.loading = true;
    this.errorMessage = '';

    this.auth.LogIn(this.email, this.password).subscribe({
      next: (response) => {
        console.log('Login successful, response:', response);
        localStorage.setItem('token', response);

        if (this.rememberMe) {
          localStorage.setItem('rememberedEmail', this.email);
        } else {
          localStorage.removeItem('rememberedEmail');
        }

        const userType = this.auth.getRoleFromToken();
        console.log('User type:', userType);
        const redirectPath = userType === 'Admin' ? '/AdminNavigation' : '/CustomerNavigation';
        this.router.navigate([redirectPath]);
      },
      error: (err) => {
        console.error('Login error:', err);
        this.errorMessage = err.error?.message || 'Invalid email or password';
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      }
    });
  }

  ResetPassword(email: string): void {
    if (!email) {
      this.message = 'Please enter your email address';
      return;
    }

    this.loading = true;
    this.message = '';

    this.auth.ResetPassword(email).subscribe({
      next: () => {
        this.message = 'Verification code has been sent to your email';
        this.codeSent = true;
        this.loading = false;
      },
      error: (err) => {
        console.error('Reset password error:', err);
        this.message = err.error?.message || 'Failed to send verification code. Please try again.';
        this.loading = false;
      }
    });
  }

  VerifyRequest(): void {
    if (!this.email || !this.code || !this.password) {
      this.message = 'Please fill all required fields';
      return;
    }

    if (this.password.length < 6) {
      this.message = 'Password must be at least 6 characters long';
      return;
    }

    this.loading = true;
    this.message = '';

    this.auth.VeritfyRequest(this.email, this.code, this.password).subscribe({
      next: () => {
        this.message = 'Password has been reset successfully! Redirecting to login...';
        setTimeout(() => {
          this.backToLogin();
        }, 2000);
      },
      error: (err) => {
        console.error('Verify request error:', err);
        this.message = err.error?.message || 'Failed to reset password. Please check the verification code.';
        this.loading = false;
      }
    });
  }

  ngOnInit(): void {
    const rememberedEmail = localStorage.getItem('rememberedEmail');
    if (rememberedEmail) {
      this.email = rememberedEmail;
      this.rememberMe = true;
    }
  }
}
