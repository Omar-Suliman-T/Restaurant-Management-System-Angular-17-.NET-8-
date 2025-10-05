import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { trigger, transition, style, animate } from '@angular/animations';
import { OrderService } from '../../services/order.service';
import { CartService } from '../../services/cart.service';
import { order, updateOrder } from '../../interFaces/Order';
import { ConfirmDialogComponent } from "../shared/confirm-dialog/confirm-dialog.component";

@Component({
  selector: 'app-order-detialse',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule, ConfirmDialogComponent],
  templateUrl: './order-detialse.component.html',
  styleUrls: ['./order-detialse.component.css'],
  animations: [
    trigger('fadeInUp', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(30px)' }),
        animate('0.5s ease', style({ opacity: 1, transform: 'translateY(0)' }))
      ])
    ]),
    trigger('slideInRight', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateX(100%)' }),
        animate('0.3s ease', style({ opacity: 1, transform: 'translateX(0)' }))
      ]),
      transition(':leave', [
        animate('0.3s ease', style({ opacity: 0, transform: 'translateX(100%)' }))
      ])
    ]),
    trigger('fadeIn', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('0.5s ease', style({ opacity: 1 }))
      ])
    ]),
    trigger('modalAnimation', [
      transition(':enter', [
        style({ opacity: 0, transform: 'scale(0.8)' }),
        animate('0.3s ease', style({ opacity: 1, transform: 'scale(1)' }))
      ]),
      transition(':leave', [
        animate('0.2s ease', style({ opacity: 0, transform: 'scale(0.8)' }))
      ])
    ])
  ]
})
export class OrderDetialsComponent implements OnInit {
  constructor(
    private orderServices: OrderService,
    private cartServices: CartService,
    private router: Router,
  ) {}

  lastOrder: order | null = null;
  showConfirm: boolean = false;
  showForm: boolean = false;
  updateOrder: updateOrder = {
    rating: null,
    deliveryAdress: null,
    customerNotes: null,
  };

  ngOnInit(): void {
    this.orderServices.GetLastOrderDetials().subscribe({
      next:(order)=>{
        this.lastOrder=order;
        if (this.lastOrder) {
          const orderTime = new Date(this.lastOrder.creationDate).getTime();
          const now = new Date().getTime();
          const timeDifference = now - orderTime;
          const hour = 60 * 60 * 1000;

          if (timeDifference > hour) {
            this.lastOrder = null;
          }
        }
        console.log(order);
      },
      error:(error)=>{
        console.log(error);
      }
    })
  }

  getStatusClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'confirmed':
        return 'confirmed';
      case 'delivered':
        return 'delivered';
      case 'cancelled':
        return 'cancelled';
      default:
        return '';
    }
  }

  UpdateOrder(): void {
    if (!this.lastOrder) return;

    this.orderServices.UpdateOrder(this.lastOrder.id, this.updateOrder).subscribe({
      next: () => {
        this.orderServices.GetLastOrderDetials().subscribe({
          next: (response) => {
            this.lastOrder = response;
            this.showForm = false;
            this.updateOrder = { rating: null, deliveryAdress: null, customerNotes: null };
          },
          error: (error) => {
            console.error('Error fetching order details:', error);
          }
        });
      },
      error: (error) => {
        console.error('Error updating order:', error);
      }
    });
  }

  onConfirmCancel(confirmed: boolean): void {
    this.showConfirm = false;
    if (confirmed) {
      this.CancelOrder();
    }
  }

  CancelOrder(): void {
    if (!this.lastOrder) return;

    this.orderServices.CancelOrder(this.lastOrder.id).subscribe({
      next: () => {
        this.cartServices.clearCartMeals();
        this.cartServices.clearCartItems();
        this.router.navigate(['/CustomerNavigation/Home']);
      },
      error: (error) => {
        console.error('Error cancelling order:', error);
      }
    });
  }
}
