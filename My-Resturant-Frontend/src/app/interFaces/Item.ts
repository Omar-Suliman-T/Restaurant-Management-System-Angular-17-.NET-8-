export interface item{
  id:number,
  name:string,
  description:string,
  price:number,
  category:string|null,
  image:string|null,
  ingrediants:Array<string>,
  stock:number,
  modificationDate:Date,
  isActive:boolean
}
export interface editItem{
  name:string|null,
  description:string|null,
  price:number|null,
  category:string|null,
  image:string|null,
  ingrediants:Array<string>|null,
  stock:number|null,
  isActive:boolean|null
}
export interface cartItem{
  id:number,
  description:string,
  name:string,
  image:string|null,
  price:number,
  quantity:number
}
