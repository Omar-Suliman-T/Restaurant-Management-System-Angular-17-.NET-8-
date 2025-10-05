import { Component, OnInit } from '@angular/core';
import { IngrediantServices } from '../../services/ingrediants.service';
import { ingrediant } from '../../interFaces/Ingrediants';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ConfirmDialogComponent } from "../shared/confirm-dialog/confirm-dialog.component";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faPlusCircle, faEdit, faTrashAlt } from '@fortawesome/free-solid-svg-icons';
import { faCamera } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-ingrediants',
  standalone: true,
  imports: [CommonModule, RouterModule, ConfirmDialogComponent, FontAwesomeModule],
  templateUrl: './ingrediants.component.html',
  styleUrls: ['../shared/shared-css/shared-table.component.css', './ingrediants.component.css']
})
export class IngrediantsComponent implements OnInit {
  faPlusCircle = faPlusCircle;
  faEdit = faEdit;
  faTrashAlt = faTrashAlt;
  faCamera = faCamera;
  ingrediants: ingrediant[] = [];
  showConfirm: boolean = false;
  ingrediantId: number = 0;

  constructor(private ingrediantServices: IngrediantServices) {}

  ngOnInit(): void {
    this.loadIngredients();
  }

  loadIngredients(): void {
    this.ingrediantServices.GetIngediants().subscribe({
      next: (theIngrediants) => {
        this.ingrediants = theIngrediants.map(ingrediant => {
          // Convert unit numbers to text
          if (Number(ingrediant.unit)) {
            switch(Number(ingrediant.unit)) {
              case 8: ingrediant.unit = 'GM'; break;
              case 9: ingrediant.unit = 'ML'; break;
              case 10: ingrediant.unit = 'PIECES'; break;
              default: ingrediant.unit = 'UNKNOWN'; break;
            }
          }
          return ingrediant;
        });
      },
      error: (err) => {
        console.error('Error loading ingredients:', err);
      }
    });
  }

  getUnitClass(unit: string): string {
    switch(unit) {
      case "GM": return 'category-fastfood';
      case "ML": return 'category-italian';
      case "PIECES": return 'category-healthy';
      default: return 'category-other';
    }
  }

  DeleteIngrediant(id: number): void {
    this.ingrediantServices.DeleteIngrediant(id).subscribe({
      next: (response) => {
        this.ingrediants = this.ingrediants.filter(ingrediant => ingrediant.id !== id);
      },
      error: (err) => {
        console.error('Error deleting ingredient:', err);
      }
    });
  }

  onConfirm(confirmed: boolean): void {
    this.showConfirm = false;
    if (confirmed) {
      this.DeleteIngrediant(this.ingrediantId);
    }
  }
}
