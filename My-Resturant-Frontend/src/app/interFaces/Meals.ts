export interface meal{
  id:number,
  name:string,
  description:string,
  image:string|null,
  category:string,
  price:number,
  stock:number,
  modificationDate:Date|null,
  isActive:boolean
}
export interface editMeal{
  name:string|null,
  description:string|null,
  image:File|null,
  category:string|null,
  stock:number|null,
  isActive:boolean|null,
  details:MealDetails[];
}
export interface theMealDetails{
  itemId:number,
  itemName:string,
  quantity:number,
  image:string
}
export interface addItemToMeal{
  itemId:number,
  mealId:number,
  quantity:number
}
export interface MealDetails{
  itemID:number|null,
  quantity:number|null
}
export interface cartMeal{
  mealId:number,
  name:string,
  image:string|null,
  price:number,
  quantity:number
}

