import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { editIngrediant, ingrediant } from '../interFaces/Ingrediants';
import {Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IngrediantServices {

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
   public GetIngediants():Observable<ingrediant[]>{
      return this.http.get<ingrediant[]>(this.Url+"/GetIngediants",{headers:this.headers})
    }
    public GetIngrediantById(id: number): Observable<editIngrediant> {
        return this.http.get<editIngrediant>(`${this.Url}/GetIngrediantById/${id}`, { headers: this.headers });
      }
    public DeleteIngrediant(id:number):Observable<any>{
      return this.http.delete(this.Url+`/DeleteIngrediant/${id}`,{headers:this.headers,responseType:'text'as const});
    }
    public UpdateIngrediant(formdata:FormData,id:number|null):Observable<any>{
      return this.http.put(this.Url+`/UpdateIngrediant/${id}`,formdata,{headers:this.headers,responseType:'text'as const})
    }
    public AddIngrediant(formdata:FormData):Observable<any>{
      return this.http.post(this.Url+'/AddIngrediant',formdata,{headers:this.headers,responseType:'text'as const})
    }


}
