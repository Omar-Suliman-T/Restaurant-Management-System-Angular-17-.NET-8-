import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { addItemToMeal, editMeal, meal, theMealDetails } from '../interFaces/Meals';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MealService {

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

        public GetMeals():Observable<meal[]>{
          return this.http.get<meal[]>(this.getBaseUrl()+"/GetMeals",{headers:this.getHeaders()})
        }
         public GetMealById(id: number): Observable<editMeal> {
          return this.http.get<editMeal>(`${this.getBaseUrl()}/GetSpecificMealDetiales/${id}`, { headers: this.getHeaders() });
        }
        public DeleteMeal(id:number):Observable<any>{
          return this.http.delete(this.getBaseUrl()+`/DeleteMeal/${id}`,{headers:this.getHeaders(),responseType:'text'as const});
        }
        public UpdateMeal(formdata:FormData,id:number|null):Observable<any>{
          return this.http.put(this.getBaseUrl()+`/UpdateMeal/${id}`,formdata,{headers:this.getHeaders(),responseType:'text'as const})
        }
        public AddMeal(formdata:FormData):Observable<any>{
          return this.http.post(this.getBaseUrl()+'/AddMeal',formdata,{headers:this.getHeaders(),responseType:'text'as const})
        }
        public GetMealItems(id:number):Observable<theMealDetails[]>{
          return this.http.get<theMealDetails[]>(this.getBaseUrl()+`/GetMealItems/${id}`,{headers:this.getHeaders()});
        }
        public AddItemToMealOrChangeQuantity(mealDetials:addItemToMeal):Observable<any>{
          return this.http.post(this.getBaseUrl()+"/AddItemToMealOrChangeQuantity",mealDetials,{headers:this.getHeaders(),responseType:'text'as const});
        }
        public DeleteItemFromMeal(mealId:number,itemId:number):Observable<any>{
          return this.http.delete(this.getBaseUrl()+`/DeleteItemFromMeal/${mealId}/${itemId}`,{headers:this.getHeaders(),responseType:'text'as const});
        }
  }

