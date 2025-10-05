import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { url } from 'inspector';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
import { code } from '../interFaces/Code';

@Injectable({
  providedIn: 'root'
})
export class CodeService {
  private myToken:string="";
  constructor(private http:HttpClient,private auth:AuthService){}

  getHeader(){

      this.myToken=this.auth.GetToken();

        return new HttpHeaders({
        'token':this.myToken,
      });

  }
  getUrl():string{
    this.myToken=this.auth.GetToken();
    if(this.auth.getRoleFromToken()==='Admin'){
      return "https://localhost:7243/Admin"
    }else{
      return "https://localhost:7243/Customer"
    }
  }


   public UpdateDiscountCode(discountCode:code):Observable<any>{
   return this.http.post(this.getUrl()+'/UpdateDiscountCode',discountCode,{headers:this.getHeader(),responseType:'text' as const})
   }

  public GetDiscountCode():Observable<string>{
   return this.http.get(this.getUrl()+'/GetDiscountCode',{headers:this.getHeader(),responseType:'text'as const})
   }








}
