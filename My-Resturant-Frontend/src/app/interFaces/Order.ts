import { cartItem } from "./Item"
import { cartMeal } from "./Meals"

export interface order{
 id:number,
 creationDate:Date,
 costumerId:number,
 customerName:string | null,
 orderStatus:string,
 deliveryAdress:string,
 costumerNotes:string|null,
 rating:string|null,
 netPrice:number,
 discountCode:string|null
}
export interface createOrGetOrder{
 costumerId:number,
 orderStatus:string|null,
 deliveryAdress:string,
 costumerNotes:string|null,
 discountCode:string|null,
 rating:string|null,
 netPrice:number,
 cartItems:cartItem[],
 cartMeals:cartMeal[]
}
export interface updateOrder{
  rating:string|null,
  deliveryAdress:string|null,
  customerNotes:string|null,

}
