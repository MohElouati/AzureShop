import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Commande } from './commande.service';


export interface Client {
  clientID?: number;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  adresses: Address[];
  commandes: Commande[];
}

export interface Address {
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
export class ClientService {

private apiUrl = 'https://localhost:7248/api/client';


  constructor(private http: HttpClient) { }

  getClients(): Observable<Client[]> {
    return this.http.get<Client[]>(this.apiUrl);
  }

  getClient(id: number): Observable<Client> {
    return this.http.get<Client>(`${this.apiUrl}/${id}`);
  }

  addClient(client: Client): Observable<Client> {
    return this.http.post<Client>(this.apiUrl, client);
  }

  deleteClient(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
  updateClient(id: number, client: Client): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, client);
  }

}
