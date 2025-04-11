import { Component, OnInit } from '@angular/core';
import { ClientService, Client } from '../../services/client.service';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { Router } from '@angular/router';

@Component({
  selector: 'app-client-list',
  standalone: true,
  templateUrl: './client-list.component.html',
  styleUrls: ['./client-list.component.css'],
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatTableModule,
    MatSnackBarModule
  ]
})
export class ClientListComponent implements OnInit {
  clients: Client[] = [];


  displayedColumns: string[] = ['firstName', 'lastName', 'dateOfBirth', 'actions'];

  constructor(
    private router: Router,
    private clientService: ClientService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadClients();
  }

  loadClients(): void {
    this.clientService.getClients().subscribe({
      next: (data) => (this.clients = data),
      error: (err) => console.error(err)
    });
  }

  navigateToAddClient(): void {
    this.router.navigate(['/clients/add']);
  }

  navigateToEditClient(id: number): void {
    this.router.navigate(['/clients/edit', id]);
  }

  navigateToClientDetail(id: number): void {
    this.router.navigate(['/clients/detail', id]);
  }

  deleteClient(id?: number): void {
    if (id === undefined) return;

    if (confirm('Êtes-vous sûr de vouloir supprimer ce client ?')) {
      this.clientService.deleteClient(id).subscribe({
        next: () => {
          this.snackBar.open('Client supprimé avec succès', 'Fermer', { duration: 3000 });
          this.loadClients();
        },
        error: (err) => console.error(err)
      });
    }
  }
}
