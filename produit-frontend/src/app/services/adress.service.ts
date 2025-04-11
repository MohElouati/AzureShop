import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Adress {
  adressID?: number;
  street: string;
  zipcode: string;
  city: string;
  country: string;
  clientID?: number;
}

@Injectable({
  providedIn: 'root'
})
export class AdressService {

private apiUrl = 'https://localhost:7248/api/Adresse';


  constructor(private http: HttpClient) {}

  getAdresses(): Observable<Adress[]> {
    return this.http.get<Adress[]>(this.apiUrl);
  }


getAllAdresses(): Observable<Adress[]> {
  return this.http.get<Adress[]>(this.apiUrl);
}

  getAdress(id: number): Observable<Adress> {
    return this.http.get<Adress>(`${this.apiUrl}/${id}`);
  }

  addAdress(adress: Adress): Observable<Adress> {
    return this.http.post<Adress>(this.apiUrl, adress);
  }

  updateAdress(id: number, adress: Adress): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, adress);
  }

  deleteAdress(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
