import { Injectable } from '@angular/core';
import { cartItem, item } from '../interFaces/Item';
import { cartMeal, meal } from '../interFaces/Meals';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartItems:cartItem[] = [];
  private cartMeals:cartMeal[] = [];
  private cartCountSubject = new BehaviorSubject<number>(0);
  cartCount$ = this.cartCountSubject.asObservable();//here any value updated in this will reflect in anywhere you are calling it.

  addItemToCart(item: cartItem):void{
    const existingItem = this.cartItems.find(theItem => theItem.id === item.id);

    if (existingItem) {
      existingItem.quantity += 1;
    } else {
      this.cartItems.push(item);
    }

    this.updateCartCount();
  }

  getCartItems(): cartItem[] {
    return this.cartItems;
  }

  removeItem(item:cartItem,allOfThem:boolean): void {
    const existingItem = this.cartItems.find(theItem => theItem.id === item.id);
        if (existingItem && !allOfThem && item.quantity > 1) {
            existingItem.quantity -= 1;
          } else {
            const index = this.cartItems.findIndex(theItem => theItem.id === item.id);
            if (index > -1) this.cartItems.splice(index, 1);
          }
          this.updateCartCount();
  }
  clearCartItems():void{
    this.cartItems=[];
    this.updateCartCount();

  }
  addMealToCart(meal: cartMeal): void {
    const existingMeal = this.cartMeals.find(m => m.mealId === meal.mealId);

    if (existingMeal) {
      existingMeal.quantity += 1;
    } else {
      this.cartMeals.push(meal);
    }

    this.updateCartCount();
  }


  getCartMeals(): cartMeal[] {
    return this.cartMeals;
  }

  removeMeal(meal:cartMeal,allOfThem:Boolean): void {
    const existingMeal = this.cartMeals.find(m => m.mealId === meal.mealId);

    if (existingMeal && !allOfThem && meal.quantity>1) {
        existingMeal.quantity -= 1;
      } else {
        const index = this.cartMeals.findIndex(m => m.mealId === meal.mealId);
        if (index > -1) this.cartMeals.splice(index, 1);
      }
      this.updateCartCount();

 }
  clearCartMeals():void{
    this.cartMeals=[];
    this.updateCartCount();
  }
  private updateCartCount(): void {
    const totalItems = this.cartItems.reduce((sum, i) => sum + i.quantity, 0);
    const totalMeals = this.cartMeals.reduce((sum, m) => sum + m.quantity, 0);
    this.cartCountSubject.next(totalItems + totalMeals);
  }



}
