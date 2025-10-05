import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { AuthService } from './auth.service';
import { cartItem, item } from '../interFaces/Item';
import { Observable } from 'rxjs';
import {jwtDecode} from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class ItemService {

   private Url:string="";
      private MyToken:string|null="";
      private headers:HttpHeaders =new HttpHeaders();
      role:string="";
      constructor(private http:HttpClient,private auth:AuthService) {}

    private getHeaders(): HttpHeaders {
    const token = this.auth.GetToken();
    return new HttpHeaders({ token: token ?? '' });
}

    private getBaseUrl(): string {
    const role = this.auth.getRoleFromToken();
    return role === 'Admin'
      ? 'https://localhost:7243/Admin'
      : 'https://localhost:7243/Customer';
}

     public GetItems():Observable<item[]>{
        return this.http.get<item[]>(this.getBaseUrl()+"/GetItems",{headers:this.getHeaders()})
      }
      public GetItemById(id: number): Observable<item> {
              return this.http.get<item>(`${this.getBaseUrl()}/GetItemById/${id}`, { headers: this.getHeaders() });
      }
      public DeleteItem(id:number):Observable<any>{
        return this.http.delete(this.getBaseUrl()+`/DeleteItem/${id}`,{headers:this.getHeaders(),responseType:'text'as const});
      }
      public UpdateItem(formdata:FormData,id:number|null):Observable<any>{
        return this.http.put(this.getBaseUrl()+`/UpdateItem/${id}`,formdata,{headers:this.getHeaders(),responseType:'text'as const})
      }
      public AddItem(formdata:FormData):Observable<any>{
        return this.http.post(this.getBaseUrl()+'/AddItem',formdata,{headers:this.getHeaders(),responseType:'text'as const})
      }
       public GetMostPopularItems():Observable<cartItem[]>{
        return this.http.get<cartItem[]>(this.getBaseUrl()+'/GetMostPopularItems',{headers:this.getHeaders()})
      }
}
