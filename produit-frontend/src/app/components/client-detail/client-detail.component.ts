import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService, Client } from '../../services/client.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatExpansionModule } from '@angular/material/expansion';



@Component({
  selector: 'app-client-detail',
  standalone: true,
  templateUrl: './client-detail.component.html',
  styleUrls: ['./client-detail.component.css'],
  imports: [CommonModule, MatCardModule, MatButtonModule, MatDividerModule, MatListModule, MatIconModule, MatExpansionModule]
})
export class ClientDetailComponent implements OnInit {
  client: Client | null = null; // ⬅️ plus clair que "undefined"

  constructor(
    private route: ActivatedRoute,
    private clientService: ClientService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    const clientId = this.route.snapshot.params['id'];
    this.clientService.getClient(clientId).subscribe({
      next: (data) => (this.client = data),
      error: (err) => console.error('Erreur lors du chargement du client', err)
    });
  }

  navigateBack(): void {
    this.router.navigate(['/clients']);
  }

  navigateToEditClient(): void {
    if (this.client?.clientID) {
      this.router.navigate(['/clients/edit', this.client.clientID]);
    } else {
      console.warn('ClientID est manquant');
    }
  }

  navigateToNewOrder(): void {
    if (this.client?.clientID) {
      this.router.navigate(['/commandes/add'], {
        queryParams: { clientID: this.client.clientID }
      });
    } else {
      console.warn('ClientID est manquant pour la commande');
    }
  }

  deleteClient(): void {
    if (this.client?.clientID && confirm('Êtes-vous sûr de vouloir supprimer ce client ?')) {
      this.clientService.deleteClient(this.client.clientID).subscribe({
        next: () => {
          this.snackBar.open('Client supprimé avec succès', 'Fermer', {
            duration: 3000
          });
          this.router.navigate(['/clients']);
        },
        error: (err) => {
          console.error('Erreur lors de la suppression', err);
          this.snackBar.open('Une erreur est survenue lors de la suppression', 'Fermer', {
            duration: 3000
          });
        }
      });
    }
  }
}
