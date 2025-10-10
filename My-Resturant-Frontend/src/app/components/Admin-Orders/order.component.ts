import { Component } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { order } from '../../interFaces/Order';
import { CommonModule, NgFor } from '@angular/common';
import { ConfirmDialogComponent } from "../shared/confirm-dialog/confirm-dialog.component";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faTrashAlt, faClipboardList } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [NgFor, CommonModule, ConfirmDialogComponent, FontAwesomeModule],
  templateUrl: './order.component.html',
  styleUrls: ['../shared/shared-css/shared-table.component.css', './order.component.css']
})
export class OrderComponent {
  faTrashAlt = faTrashAlt;
  faClipboardList = faClipboardList;

  Orders: order[] = [];
  showConfirm: boolean = false;
  orderId: number = 0;

  constructor(private OrderServices: OrderService,private authservices:AuthService) {}

  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders(): void {

    this.OrderServices.GetOrders().subscribe({
      next: (theOrders) => {
        this.Orders = theOrders;
        console.log(theOrders);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  DeleteOrder(Id: number): void {
    this.OrderServices.CancelOrder(Id).subscribe({
      next: (response) => {
        this.Orders = this.Orders.filter(order => order.id != Id);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  onConfirm(confirmed: boolean): void {
    this.showConfirm = false;
    if (confirmed) {
      this.DeleteOrder(this.orderId);
    }
  }

  getStatusClass(status: string): string {
    if (!status) return 'status-pending';

    switch(status) {
      case 'Confirmed':
        return 'status-pending';
      case 'Delivered':
        return 'status-completed';
      case 'Cancelled':
        return 'status-cancelled';
      default:
        return 'status-pending';
    }
  }

  getRatingClass(rating: string | null): string {
    if (!rating) return 'rating-no-rating';

    switch(rating) {
      case 'Bad':
        return 'rating-bad';
      case 'Good':
        return 'rating-good';
      case 'Very Good':
        return 'rating-very-good';
      case 'Exillent':
        return 'rating-excellent';
      default:
        return 'rating-no-rating';
    }
  }
}
