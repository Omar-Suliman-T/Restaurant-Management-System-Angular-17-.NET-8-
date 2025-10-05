export interface user{
  id:number,
  firstName:string,
  lastName:string,
  isActive:boolean,
  creationDate:Date,
  role:string,
  email:string,
  password:string,
  phone:string
}
export interface editUser{
  firstName:string|null,
  lastName:string|null,
  isActive:boolean|null,
  role:string|null,
  email:string|null,
  password:string|null,
  phone:string|null
}
export interface signUpUser{
  firstName:string,
  lastName:string,
  email:string,
  password:string,
  phone:string
}

