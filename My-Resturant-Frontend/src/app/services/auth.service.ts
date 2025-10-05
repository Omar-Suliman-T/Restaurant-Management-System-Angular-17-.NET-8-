import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { url } from 'inspector';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import {jwtDecode} from 'jwt-decode';
import { text } from 'stream/consumers';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http:HttpClient) { }
  Url:string='https://localhost:7243/Auth';
  MyToken:string|null="";

     headers =new HttpHeaders({
    "Authorization":`Beaere ${this.MyToken}`
  });

  public LogIn(email:string,password:string):Observable<string>{
   return this.http.post<string>(this.Url+"/LogIn",{email,password},{ responseType: 'text' as 'json' });
  }
   public LogOut(){
    this.MyToken= this.GetToken();

    this.http.post(this.Url+"/LogOut",null,{headers:this.headers});
    localStorage.removeItem('token');
   }

   public ResetPassword(email:string):Observable<any>{
    return this.http.post(this.Url+'/ResetRequest',{email})
   }

   public VeritfyRequest(email:string,code:string,newPassword:string):Observable<any>{
    return this.http.post(this.Url+'/VertifyReset',{email,code,newPassword},{responseType:'text' as const})
   }



   public GetToken(){
    return localStorage.getItem('token')??"";
  }
   public getRoleFromToken(): string {
    const decoded:any = jwtDecode(this.GetToken()??"");
    return decoded.role;
 }
   public getIdFromToken(): number {
    const decoded:any = jwtDecode(this.GetToken()??"");
    return decoded.PersonId;
 }
}
