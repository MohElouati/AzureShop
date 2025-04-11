import { Component, OnInit } from '@angular/core';
import { CommandeService, Commande } from '../../services/commande.service';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-commande-list',
  standalone: true,
  templateUrl: './commande-list.component.html',
  styleUrls: ['./commande-list.component.css'],
  imports: [
    CommonModule,
    MatTableModule,
    MatCardModule,
    MatButtonModule,
    MatSnackBarModule
  ]
})
export class CommandeListComponent implements OnInit {
  commandes: Commande[] = [];


  displayedColumns: string[] = ['numCommande', 'dateCommande', 'clientID', 'produits', 'actions'];

  constructor(
    private commandeService: CommandeService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadCommandes();
  }

  loadCommandes(): void {
    this.commandeService.getCommandes().subscribe({
      next: (data) => (this.commandes = data),
      error: (err) => console.error(err)
    });
  }

  deleteCommande(id: number): void {
    if (confirm('Êtes-vous sûr de vouloir supprimer cette commande ?')) {
      this.commandeService.deleteCommande(id).subscribe({
        next: () => {
          this.snackBar.open('Commande supprimée avec succès', 'Fermer', { duration: 3000 });
          this.loadCommandes();
        },
        error: (err) => console.error(err)
      });
    }
  }
  isLast(list: any[], item: any): boolean {
    return list.indexOf(item) === list.length - 1;
  }

  navigateToAddCommande(): void {
    this.router.navigate(['/commandes/add']);
  }

  navigateToEditCommande(id: number): void {
    this.router.navigate(['/commandes/edit', id]);
  }

  navigateToDetailClient(id: number): void {
    this.router.navigate(['/clients/detail', id]);
  }
}
