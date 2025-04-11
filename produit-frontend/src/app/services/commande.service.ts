import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from './product.service';

export interface Commande {
  numCommande?: number;
  date?: string;
  clientID: number;
  commandeProduits: CommandeProduits[];
}

export interface CommandeProduits {
  productID: number;
  produitID: number;
  quantity: number;
  produit?: Product;
}

export interface Client {
  clientID: number;
  firstName: string;
  lastName: string;
}

@Injectable({
  providedIn: 'root'
})
export class CommandeService {
  private apiUrl = 'https://localhost:7248/api/commande';
  private clientsUrl = 'https://localhost:7248/api/client'; // ✅ Endpoint pour récupérer les clients

  constructor(private http: HttpClient) { }

  getCommandes(): Observable<Commande[]> {
    return this.http.get<Commande[]>(this.apiUrl);
  }

  getCommande(id: number): Observable<Commande> {
    return this.http.get<Commande>(`${this.apiUrl}/${id}`);
  }

  addCommande(commande: Commande): Observable<Commande> {
    return this.http.post<Commande>(this.apiUrl, commande);
  }
  updateCommande(id: number, commande: Commande): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, commande);
  }


  deleteCommande(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getClients(): Observable<Client[]> {  // ✅ Récupérer la liste des clients
    return this.http.get<Client[]>(this.clientsUrl);
  }
}
