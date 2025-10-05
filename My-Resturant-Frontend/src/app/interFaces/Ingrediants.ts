export interface ingrediant{
  id:number,
  name:string,
  unit:string,
  image:string,
  isActive:boolean,
  creationDate:Date,
  modificationDate:Date
}
export interface editIngrediant{
  name:string|null,
  unit:string|null,
  isActive:boolean|null,
}
