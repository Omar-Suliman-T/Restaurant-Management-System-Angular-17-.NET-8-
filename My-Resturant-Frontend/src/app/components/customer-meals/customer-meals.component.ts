import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { trigger, transition, style, animate } from '@angular/animations';
import { cartMeal, meal, theMealDetails } from '../../interFaces/Meals';
import { MealService } from '../../services/meal.service';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-customer-meals',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './customer-meals.component.html',
  styleUrls: ['./customer-meals.component.css'],
  animations: [
    trigger('cardAnimation', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(20px)' }),
        animate('0.5s ease', style({ opacity: 1, transform: 'translateY(0)' }))
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
    ]),
    trigger('itemAnimation', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateX(-20px)' }),
        animate('0.3s ease', style({ opacity: 1, transform: 'translateX(0)' }))
      ])
    ])
  ]
})
export class CustomerMealsComponent implements OnInit {
  meals: meal[] = [];
  mealDetials: theMealDetails[] = [];
  theButtonId: number = 0;
  showDetials: boolean = false;
  cartCount: number = 0;

  constructor(
    private mealServices: MealService,
    private cartServices: CartService
  ) {}

  ngOnInit(): void {
    this.mealServices.GetMeals().subscribe({
      next: (response) => {
        this.meals = response;
      },
      error: (error) => {
        console.log(error);
      }
    });

       this.cartServices.cartCount$.subscribe({
      next:(count)=>{
        this.cartCount=count;
      }
    })

  }

  addItemToCart(meal: meal) {
    const cartMeal:cartMeal={
      mealId: meal.id,
      name: meal.name,
      image: meal.image,
      price: meal.price,
      quantity: 1
    }
    this.cartServices.addMealToCart(cartMeal);
    this.theButtonId = meal.id;

    setTimeout(() => {
      this.theButtonId = 0;
    }, 1500);
  }

  GetMealDetials(mealId: number) {
    this.showDetials = true;
    this.mealServices.GetMealItems(mealId).subscribe({
      next: (response) => {
        this.mealDetials = response;
      },
      error: (error) => {
        console.log(error);
      }
    });
  }
}
