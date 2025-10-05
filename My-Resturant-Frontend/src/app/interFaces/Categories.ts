export interface category{
  name:string,
  description:string,
  id:number,
  isActive:boolean,
  creationDate:Date,
  modificationDate:Date
}
export interface editCategory{
  name:string|null,
  description:string|null,
  isActive:boolean|null,
}
