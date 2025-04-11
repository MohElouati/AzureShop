import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProductService, Product } from '../../services/product.service';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-product-list',
  standalone: true,
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatTableModule,
    MatSnackBarModule
  ]
})
export class ProductListComponent implements OnInit {

  displayedColumns: string[] = ['nom', 'description', 'prix', 'poids','actions'];
  products: Product[] = [];

  constructor(
    private productService: ProductService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe({
      next: (data) => (this.products = data),
      error: (err) => console.error(err)
    });
  }

  navigateToProductDetail(produitID?: number): void {
    if (produitID !== undefined) {
      this.router.navigate(['/products/detail', produitID]);
    } else {
      console.error('Produit ID is undefined');
    }
  }

  navigateToEditProduct(produitID?: number): void {
    if (produitID !== undefined) {
      this.router.navigate(['/products/edit', produitID]);
    } else {
      console.error('Produit ID is undefined');
    }
  }

  navigateToAddProduct(): void {
    this.router.navigate(['/products/add']);
  }

  deleteProduct(produitID?: number): void {
    if (produitID !== undefined) {
      this.productService.deleteProduct(produitID).subscribe({
        next: () => this.loadProducts(),
        error: (err) => console.error(err)
      });
    } else {
      console.error('Produit ID is undefined');
    }
  }


  public goToCommandes(): void {
    this.router.navigate(['/commandes']);
  }

  public goToClients(): void {
    this.router.navigate(['/clients']);
  }

  public goToProduits(): void {
    this.router.navigate(['/products']);
  }
}
