import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray, ReactiveFormsModule } from '@angular/forms';
import { ClientService, Client } from '../../services/client.service';
import { AdressService, Adress } from '../../services/adress.service';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatDividerModule } from '@angular/material/divider';

@Component({
  selector: 'app-client-form',
  standalone: true,
  templateUrl: './client-form.component.html',
  styleUrls: ['./client-form.component.css'],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatDividerModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
    MatSelectModule,
    MatSnackBarModule,
    RouterModule
  ]
})
export class ClientFormComponent implements OnInit {
  clientForm!: FormGroup;
  clientID?: number;

  constructor(
    private fb: FormBuilder,
    private clientService: ClientService,
    private addressService: AdressService,
    private router: Router,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.clientForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      adresses: this.fb.array([]) // ✅ this key must match the backend
    });

    this.addAddress();

    this.clientID = this.route.snapshot.params['id'];
    if (this.clientID) {
      this.clientService.getClient(this.clientID).subscribe(client => {
        this.clientForm.patchValue({
          firstName: client.firstName,
          lastName: client.lastName,
          dateOfBirth: client.dateOfBirth
        });

        this.adresses.clear();
        client.adresses.forEach((a: Adress) => this.addAddress(a));
      });
    }
  }

  get adresses(): FormArray {
    return this.clientForm.get('adresses') as FormArray;
  }

  addAddress(address: Adress | null = null): void {
    this.adresses.push(this.fb.group({
      street: [address?.street || '', Validators.required],
      zipcode: [address?.zipcode || '', Validators.required],
      city: [address?.city || '', Validators.required],
      country: [address?.country || '', Validators.required]
    }));
  }

  removeAddress(index: number): void {
    this.adresses.removeAt(index);
  }

  saveClient(): void {
    if (this.clientForm.invalid || this.adresses.length === 0) {
      this.snackBar.open('Veuillez remplir toutes les informations, y compris une adresse.', 'Fermer', { duration: 3000 });
      return;
    }

    const client: Client = this.clientForm.value;

    if (this.clientID) {
      this.clientService.updateClient(this.clientID, client).subscribe({
        next: () => {
          this.snackBar.open('Client mis à jour avec succès', 'Fermer', { duration: 3000 });
          this.router.navigate(['/clients']);
        },
        error: (err) => console.error(err)
      });
    } else {
      this.clientService.addClient(client).subscribe({
        next: () => {
          this.snackBar.open('Client ajouté avec succès', 'Fermer', { duration: 3000 });
          this.router.navigate(['/clients']);
        },
        error: (err) => console.error(err)
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/clients']);
  }
}
