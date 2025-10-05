import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { promises } from 'dns';
import { Reservation } from '../interFaces/Reservation';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class reservationServices {
  private Url:string="";
  private role:string="";
  private MyToken:string|null="";
  private headers:HttpHeaders = new HttpHeaders();

  constructor(private http:HttpClient,private auth:AuthService) {}

  private getHeaders(): HttpHeaders {
  const token = this.auth.GetToken();
  return new HttpHeaders({ token: token ?? '' });
}

  private getUrl(): string {
  const role = this.auth.getRoleFromToken();
  return role === 'Admin'
    ? 'https://localhost:7243/Admin'
    : 'https://localhost:7243/Customer';
}

  public async GetMyReservation(customerId:number):Promise<Reservation | undefined>{
    return await this.http.get<Reservation>(this.getUrl()+`/GetMyReservation/${customerId}`,{headers:this.getHeaders()}).toPromise()
  }
  public GetAllReservations():Observable<Reservation[]>{
    return this.http.get<Reservation[]>(this.getUrl()+"/GetAllReservations",{headers:this.getHeaders()});
  }
  public UpdateReservation(UpdateReservation:Reservation):Observable<any>{
    return this.http.put(this.getUrl()+"/UpdateReservation",UpdateReservation,{headers:this.getHeaders(), responseType:'text'as const})
  }
  public CreateReservation(reservation:Reservation):Observable<any>{
    return this.http.post(this.getUrl()+'/CreateReservation',reservation,{headers:this.getHeaders(),responseType:'text'as const})
  }
  public DeleteReservation(reservationId:number):Observable<any>{
    return this.http.delete(this.getUrl()+`/DeleteReservation/${reservationId}`,{headers:this.getHeaders(),responseType:'text'as const})
  }


}
