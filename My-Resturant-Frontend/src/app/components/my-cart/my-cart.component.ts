import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { trigger, transition, style, animate, stagger, query } from '@angular/animations';
import { CartService } from '../../services/cart.service';
import { AuthService } from '../../services/auth.service';
import { OrderService } from '../../services/order.service';
import { cartMeal,} from '../../interFaces/Meals';
import { cartItem } from '../../interFaces/Item';
import { createOrGetOrder } from '../../interFaces/Order';
import { CodeService } from '../../services/code.service';

@Component({
  selector: 'app-my-cart',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './my-cart.component.html',
  styleUrls: ['./my-cart.component.css'],
  animations: [
    trigger('itemAnimation', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateX(-20px)' }),
        animate('0.3s ease', style({ opacity: 1, transform: 'translateX(0)' }))
      ]),
      transition(':leave', [
        animate('0.2s ease', style({ opacity: 0, transform: 'translateX(20px)' }))
      ])
    ]),
    trigger('fadeIn', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('0.5s ease', style({ opacity: 1 }))
      ])
    ])
  ]
})
export class MyCartComponent implements OnInit {
  cartMeals: cartMeal[] = [];
  cartItems: cartItem[] = [];
  totalPrice: number = 0;
  notes: string = "";
  location: string | null = null;
  order: createOrGetOrder;
  token: string | null = null;
  isOrderEmpty: boolean = false;
  isLocationEmpty: boolean = false;
  isSubmitting: boolean = false;
  wrongCode:boolean=false;
  discountCode:string|null=null;

  constructor(
    private cartServices: CartService,
    private authServices: AuthService,
    private orderServices: OrderService,
    private router: Router,
    private codeServices:CodeService
  ) {
    this.order = {
      costumerId: 0,
      orderStatus: null,
      deliveryAdress: "",
      costumerNotes: null,
      discountCode:null,
      rating: null,
      netPrice: 0,
      cartItems: [],
      cartMeals: []
    };
  }

  ngOnInit(): void {
    this.loadCartItems();
    this.loadDiscountCode();
    this.token = this.authServices.GetToken();
  }
  loadDiscountCode(){
    this.codeServices.GetDiscountCode().subscribe({
      next:(theCode)=>{
        this.discountCode=theCode;
      },error:(err)=>{
        console.log(err);
      }
    })
  }
  loadCartItems(): void {
    this.cartMeals = this.cartServices.getCartMeals();
    this.cartItems = this.cartServices.getCartItems();
    this.calculateTotalPrice();
  }

  calculateTotalPrice(): void {
    this.totalPrice = 0;

    this.cartMeals.forEach(meal => {
      this.totalPrice += (meal.price ?? 0) * (meal.quantity ?? 1);
    });

    this.cartItems.forEach(item => {
      this.totalPrice += (item.price ?? 0) * (item.quantity ?? 1);
    });
  }

  getTotalItems(): number {
    return this.cartMeals.reduce((total, meal) => total + meal.quantity, 0) +
           this.cartItems.reduce((total, item) => total + item.quantity, 0);
  }

  increaseMealQuantity(meal: cartMeal): void {

    this.cartServices.addMealToCart(meal);
    this.ngOnInit();
    this.calculateTotalPrice();
  }

  decreaseMealQuantity(meal: cartMeal): void {
      this.cartServices.removeMeal(meal,false);
      this.ngOnInit();
      this.calculateTotalPrice();
  }

  increaseItemQuantity(item: cartItem): void {
    this.cartServices.addItemToCart(item);
    this.ngOnInit();
    this.calculateTotalPrice();
  }

  decreaseItemQuantity(item: cartItem): void {
      this.cartServices.removeItem(item,false);
      this.ngOnInit();
      this.calculateTotalPrice();
  }

  removeMeal(meal: cartMeal): void {
      this.cartServices.removeMeal(meal,true);
      this.calculateTotalPrice();
    }


  removeItem(item: cartItem): void {
      this.cartServices.removeItem(item,true);
      this.calculateTotalPrice();
    }


  clearCart(): void {
    this.cartMeals = [];
    this.cartItems = [];
    this.cartServices.clearCartMeals();
    this.cartServices.clearCartItems();
    this.calculateTotalPrice();
  }

  async ConfirmOrder(): Promise<void> {
    this.isOrderEmpty = false;
    this.isLocationEmpty = false;
    if(this.order.discountCode!==null && this.discountCode!==null && this.discountCode!==this.order.discountCode){
      this.wrongCode=true;
      return;
    }
    if (this.cartMeals.length === 0 && this.cartItems.length === 0) {
      this.isOrderEmpty = true;
      return;
    }

    if (!this.location || this.location.trim() === '') {
      this.isLocationEmpty = true;
      return;
    }

    this.isSubmitting = true;

    if(this.order.discountCode!==null && this.order.netPrice>30){
      this.order.netPrice=this.order.netPrice*0.8
    }
    this.order.costumerId = this.authServices.getIdFromToken();
    this.order.costumerNotes = this.notes;
    this.order.deliveryAdress = this.location ?? "";
    this.order.orderStatus = "confirmed";
    this.order.netPrice = this.totalPrice;
    this.order.rating = "Not Rated Yet";
    this.order.cartItems = this.cartItems;
    this.order.cartMeals = this.cartMeals;

    try {
      await this.orderServices.CreateOrder(this.order).toPromise();
      this.cartServices.clearCartMeals();
      this.cartServices.clearCartItems();
      this.router.navigate(['/CustomerNavigation/OrderDetiales']);
    } catch (error) {
      console.error('Order submission error:', error);
    } finally {
      this.isSubmitting = false;
    }
  }
}
