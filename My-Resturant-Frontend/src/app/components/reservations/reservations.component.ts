import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { trigger, transition, style, animate } from '@angular/animations';
import { reservationServices } from '../../services/reservation.service';
import { AuthService } from '../../services/auth.service';
import { Reservation } from '../../interFaces/Reservation';
import { ConfirmDialogComponent } from "../shared/confirm-dialog/confirm-dialog.component";

@Component({
  selector: 'app-reservations',
  standalone: true,
  imports: [CommonModule, FormsModule, ConfirmDialogComponent],
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css'],
  animations: [
    trigger('fadeInUp', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(20px)' }),
        animate('0.5s ease', style({ opacity: 1, transform: 'translateY(0)' }))
      ])
    ]),
    trigger('fadeIn', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('0.3s ease', style({ opacity: 1 }))
      ])
    ]),
    trigger('slideIn', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateX(-20px)' }),
        animate('0.3s ease', style({ opacity: 1, transform: 'translateX(0)' }))
      ])
    ]),
    trigger('slideInUp', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(30px)' }),
        animate('0.4s ease', style({ opacity: 1, transform: 'translateY(0)' }))
      ])
    ]),
    trigger('slideInDown', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(-30px)' }),
        animate('0.3s ease', style({ opacity: 1, transform: 'translateY(0)' }))
      ])
    ])
  ]
})
export class ReservationsComponent implements OnInit {
  availableTimes: string[] = [
    '13:00', '14:00', '15:00', '16:00', '17:00',
    '18:00', '19:00', '20:00', '21:00', '22:00'
  ];

  role: string = "";
  customerId: number = 0;
  ReservationExist: boolean = false;
  Reservations: Reservation[] | undefined = [];
  adminReservations: boolean = false;
  customerReservation: boolean = false;
  ReservationForm: boolean = false;
  showNoReservation: boolean = false;
  isLoading: boolean = true;
  show: boolean = false;
  showConfirm:boolean=false;
  name:string="";

  // Notification properties
  showNotification: boolean = false;
  notificationMessage: string = "";
  notificationType: string = "error";
  notificationIcon: string = "fas fa-exclamation-circle";

  minDate: string = new Date().toISOString().split('T')[0];

  myReservation: Reservation = {
    reservationId: null,
    creationDate: null,
    status: null,
    numberOfPeople: null,
    specialRequests: null,
    reservationTime: null,
    reservationDate: null,
    customerId:null,
    customerFirstName:null,
    customerLastName:null
  }

  constructor(
    private reservationServices: reservationServices,
    private authService: AuthService
  ) {}

  async ngOnInit() {
    this.role = this.authService.getRoleFromToken();
    console.log(this.role);
    this.customerId = this.authService.getIdFromToken();

    if (this.role === "Admin") {
      await this.loadAdminReservations();
    } else {
      await this.loadCustomerReservation();
    }
    this.isLoading = false;
  }

  private async loadAdminReservations(): Promise<void> {
    this.myReservation = Object.keys(this.myReservation).reduce((acc, key) => {
        (acc as any)[key] = null;
        return acc;
      }, {} as typeof this.myReservation);
    this.ReservationExist=false;

    try {
      this.Reservations = await this.reservationServices.GetAllReservations().toPromise();
      if(this.Reservations){
        this.adminReservations = true;
      }
      else{
        this.showNoReservation=true;
      }
    } catch (error) {
      console.error('Error loading reservations:', error);
      this.showNotificationMessage('Error loading reservations', 'error');
      this.showNoReservation=true;
    }
  }

  private async loadCustomerReservation(): Promise<void> {
    try {
      const theReservation = await this.reservationServices.GetMyReservation(this.customerId);
      if (theReservation) {
        this.myReservation = theReservation;
        this.name=this.myReservation.customerFirstName+''+this.myReservation.customerLastName;
        this.customerReservation = true;
        this.ReservationExist = true;
        this.showNoReservation = false;
      } else {
        this.showNoReservation = true;
        this.customerReservation = false;
      }
    } catch (error) {
      console.error('Error loading customer reservation:', error);
      this.showNotificationMessage('Error loading your reservation', 'error');
      this.showNoReservation = true;
    }
  }

  openReservationForm(): void {
    this.ReservationForm = true;
    this.adminReservations = false;
    this.customerReservation = false;
    this.showNoReservation = false;

    if (!this.ReservationExist) {
      this.myReservation = {
        reservationId: 0,
        creationDate: null,
        status: 'Confirmed',
        numberOfPeople: 2,
        specialRequests: null,
        reservationTime: null,
        reservationDate: null,
        customerId:this.customerId,
        customerFirstName:null,
        customerLastName:null
      };
    }
  }

  closeReservationForm(): void {
    this.ReservationForm = false;

    if (this.role === "Admin") {
      this.loadAdminReservations();
    } else if (this.ReservationExist) {
      this.loadCustomerReservation();
    } else {
      this.showNoReservation = true;
    }
  }

  CreateReservation(): void {
    if (this.validateReservationForm()) {
      this.isLoading = true;
      this.reservationServices.CreateReservation(this.myReservation).subscribe({
        next: (response) => {
          this.showNotificationMessage('Reservation created successfully!', 'success');
          this.ReservationForm = false;
          this.ReservationExist = true;
          this.role==='Admin'? this.loadAdminReservations(): this.loadCustomerReservation();
          this.isLoading = false;
        },
        error: (error) => {
          console.error('Error creating reservation:', error);
          this.showNotificationMessage('Error creating reservation. Please try again.', 'error');
          this.isLoading = false;
        }
      });
    }
  }

  UpdateReservation(): void {
    if (this.validateReservationForm()) {
      this.isLoading = true;
      this.reservationServices.UpdateReservation(this.myReservation).subscribe({
        next: (response) => {
          this.showNotificationMessage('Reservation updated successfully!', 'success');
          this.ReservationForm = false;
          this.role==='Admin'? this.loadAdminReservations(): this.loadCustomerReservation();
          this.isLoading = false;
        },
        error: (error) => {
          console.error('Error updating reservation:', error);
          this.showNotificationMessage('Error updating reservation. Please try again.', 'error');
          this.isLoading = false;
        }
      });
    }
  }

  private validateReservationForm(): boolean {
    let isValid = true;

    if (!this.myReservation.reservationDate) {
      this.showNotificationMessage('Please select a reservation date', 'warning');
      isValid = false;
    }

    if (!this.myReservation.reservationTime) {
      this.showNotificationMessage('Please select a reservation time', 'warning');
      isValid = false;
    }

    if (!this.myReservation.numberOfPeople || this.myReservation.numberOfPeople < 1) {
      this.showNotificationMessage('Please enter a valid number of people', 'warning');
      isValid = false;
    }
     if (!this.myReservation.customerFirstName && this.role==="Admin") {
      this.showNotificationMessage('Please enter a valid Customer First Name', 'warning');
      isValid = false;
    }
    if (!this.myReservation.customerLastName && this.role==="Admin") {
      this.showNotificationMessage('Please enter a valid Customer Last Name', 'warning');
      isValid = false;
    }
    return isValid;
  }

  CancellReservation(reservationId: number): void {

      this.isLoading = true;
      this.reservationServices.DeleteReservation(reservationId).subscribe({
        next: (response) => {
          this.showNotificationMessage('Reservation cancelled successfully', 'success');
          if (this.role === "Admin") {
            this.loadAdminReservations();
          } else {
            this.ReservationExist = false;
            this.customerReservation = false;
            this.showNoReservation = true;
          }
          this.isLoading = false;
        },
        error: (error) => {
          console.error('Error cancelling reservation:', error);
          this.showNotificationMessage('Error cancelling reservation. Please try again.', 'error');
          this.isLoading = false;
        }
      });
    }


  PrepareReservation(theReservation: Reservation): void {
    this.myReservation = { ...theReservation };
    this.ReservationExist = true;
  }

  // Notification methods
  showNotificationMessage(message: string, type: string = 'error'): void {
    this.notificationMessage = message;
    this.notificationType = type;

    switch (type) {
      case 'success':
        this.notificationIcon = 'fas fa-check-circle';
        break;
      case 'warning':
        this.notificationIcon = 'fas fa-exclamation-triangle';
        break;
      default:
        this.notificationIcon = 'fas fa-exclamation-circle';
    }

    this.showNotification = true;

    // Auto-hide after 5 seconds
    setTimeout(() => {
      this.hideNotification();
    }, 5000);
  }

  hideNotification(): void {
    this.showNotification = false;
  }

  onConfirm(confirmed: boolean): void {
    this.showConfirm = false;
    if (confirmed) {
      this.CancellReservation(this.myReservation.reservationId!);
    }
  }
  getStatus(status: string | null): string {
    switch (status) {
      case "Completed":
        return "status-completed";
      case "Cancelled":
        return "status-cancelled";
      case "Confirmed":
        return "status-confirmed";
      case "Pending":
        return "status-pending";
      default:
        return "status-pending";
    }
  }

  getStatusIcon(status: string | null): string {
    switch (status) {
      case "Completed":
        return "fas fa-circle-check text-green";
      case "Cancelled":
        return "fas fa-times-circle text-red";
      case "Confirmed":
        return "fas fa-check-circle text-blue";
      case "Pending":
        return "fas fa-clock text-orange";
      default:
        return "fas fa-clock text-orange";
    }
  }
}
