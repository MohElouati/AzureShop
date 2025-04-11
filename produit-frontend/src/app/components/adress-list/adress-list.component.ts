import { Component, OnInit } from '@angular/core';
import { AdressService, Adress } from '../../services/adress.service';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';

@Component({
  standalone: true,
  selector: 'app-adress-list',
  templateUrl: './adress-list.component.html',
  styleUrls: ['./adress-list.component.css'],
  imports: [CommonModule, MatCardModule, MatTableModule] // 👈 ajout des modules
})
export class AdressListComponent implements OnInit {
  adresses: Adress[] = []; // 👈 nom pluriel + typage correct

  constructor(private adressService: AdressService) {}

  ngOnInit(): void {
    this.adressService.getAllAdresses().subscribe(data => {
      this.adresses = data;
    });


  }
}
