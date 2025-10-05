import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { user } from '../interFaces/User';
import {createOrGetOrder, order, updateOrder } from '../interFaces/Order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
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
    public CreateOrder(order:createOrGetOrder):Observable<any>{
      return this.http.post(this.getBaseUrl()+"/CreateOrder",order,{headers:this.getHeaders(),responseType:'text' as 'json'})
    }
    public UpdateOrder(orderId:number,order:updateOrder):Observable<any>{
      return this.http.put(this.getBaseUrl()+`/UpdateOrder/${orderId}`,order,{headers:this.getHeaders(),responseType:'text' as 'json'})
    }
    public GetLastOrderDetials():Observable<order>{
      return this.http.get<order>(this.getBaseUrl()+"/GetLastOrderDetiales",{headers:this.getHeaders()})
    }
    public GetOrders():Observable<order[]>{
      return this.http.get<order[]>(this.getBaseUrl()+"/GetAllOrders",{headers:this.getHeaders()})
    }
    public CancelOrder(id:number):Observable<any>{
      return this.http.delete(this.getBaseUrl()+`/CancelOrder/${id}`,{headers:this.getHeaders(),responseType:'text' as 'json'});
}


}
