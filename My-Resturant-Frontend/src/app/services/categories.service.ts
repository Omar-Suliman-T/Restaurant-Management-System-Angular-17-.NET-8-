import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { category, editCategory } from '../interFaces/Categories';
import {Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {
  private Url:string="https://localhost:7243/Admin";
  private MyToken:string|null="";
  private headers:HttpHeaders =new HttpHeaders();

  constructor(private http:HttpClient,private auth:AuthService) {
     this.MyToken=auth.GetToken();
    if(this.MyToken){
        this.headers=new HttpHeaders({
          "token": this.MyToken
        });
    }}
  public getCategories():Observable<category[]>{
    return this.http.get<category[]>(this.Url+"/GetCategories",{headers:this.headers})
  }
    public GetCategoryById(id: number): Observable<category> {
    return this.http.get<category>(`${this.Url}/GetCategoryById/${id}`, { headers: this.headers });
  }
  public DeleteCategory(id:number):Observable<any>{
    return this.http.delete(this.Url+`/DeleteCategory/${id}`,{headers:this.headers,responseType:'text'as const});
  }
  public UpdateCategory(category:editCategory,id:number|null):Observable<any>{
    return this.http.put(this.Url+`/UpdateCategory/${id}`,category,{headers:this.headers,responseType:'text'as const})
  }
  public AddCategory(category:editCategory):Observable<any>{
    return this.http.post(this.Url+'/AddCategory',category,{headers:this.headers,responseType:'text'as const})
  }








}
