import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { editUser, signUpUser, user } from '../interFaces/User';
import { observable, Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { AuthService } from './auth.service';
import test from 'node:test';
@Injectable({
  providedIn: 'root'
})
export class userServices {
  private AdminUrl:string="https://localhost:7243/Admin";
  private CustomerUrl:string="https://localhost:7243/Customer";
  private MyToken:string|null="";
  private headers:HttpHeaders =new HttpHeaders();

  constructor(private http:HttpClient,private auth:AuthService) {}

   getHeader():any{
    this.MyToken=this.auth.GetToken();
    if(this.MyToken){
       return this.headers=new HttpHeaders({
          "token": this.MyToken
        });
    }
  }
  public getUsers():Observable<user[]>{
    return this.http.get<user[]>(this.AdminUrl+"/GetPeople",{headers:this.getHeader()})
  }
  public DeletePerson(id:number):Observable<any>{
    return this.http.delete(this.AdminUrl+`/DeletePerson/${id}`,{headers:this.getHeader()});
  }
  public UpdatePerson(user:editUser,id:number|null):Observable<any>{
    return this.http.put(this.AdminUrl+`/UpdatePerson/${id}`,user,{headers:this.getHeader(),responseType:'text'as const})
  }
  public AddUser(user:editUser):Observable<any>{
    return this.http.post(this.AdminUrl+'/CreatPreson',user,{headers:this.getHeader(),responseType:'text'as const})
  }
  public AddCustomer(user:signUpUser):Observable<any>{
    return this.http.post(this.CustomerUrl+'/AddCustomer',user,{responseType:'text' as 'json'})
  }
  public GetPersonById(id: number): Observable<editUser> {
  return this.http.get<editUser>(`${this.AdminUrl}/GetPersonById/${id}`, { headers: this.getHeader() });
}


}

