import { Component, OnInit } from '@angular/core';
import { editIngrediant } from '../../interFaces/Ingrediants';
import { IngrediantServices } from '../../services/ingrediants.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faSave, faTimes } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-ingrediant-form',
  standalone: true,
  imports: [FormsModule, CommonModule, FontAwesomeModule],
  templateUrl: './ingrediant-form.component.html',
  styleUrls: ['./ingrediant-form.component.css','../shared/shared-css/shared-forms.css']
})
export class IngrediantFormComponent implements OnInit {
  faSave = faSave;
  faTimes = faTimes;

  ingrediant: editIngrediant = {
    name: null,
    unit: null,
    isActive: null,
  }

  isRequaired: boolean = false;
  ingrediantId: number | null = null;
  selectedImage: File | null = null;

  constructor(
    private ingrediantServices: IngrediantServices,
    private route: ActivatedRoute,
    public router: Router
  ) {}

  ngOnInit(): void {
    this.ingrediantId = Number(this.route.snapshot.paramMap.get('id'));
    this.isRequaired = this.ingrediantId === 0;

    if (this.ingrediantId && this.ingrediantId !== 0) {
      this.loadIngredientData(this.ingrediantId);
    }
  }

  loadIngredientData(id: number): void {
    this.ingrediantServices.GetIngrediantById(id).subscribe({
      next: (ingredient) => {
        this.ingrediant = ingredient;
      },
      error: (err) => {
        console.error('Error loading ingredient:', err);
      }
    });
  }

  onImageSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.selectedImage = file;
    }
  }

  UpdateIngrediant(): void {
    if (this.isRequaired) {
      const hasNull = Object.values(this.ingrediant).some(value => value === null);
      if (hasNull) return;
    }

    const formData = new FormData();
    if (this.selectedImage) formData.append('image', this.selectedImage);
    if (this.ingrediant.name) formData.append('name', this.ingrediant.name);
    if (this.ingrediant.unit) formData.append('unit', this.ingrediant.unit);
    if (this.ingrediant.isActive !== null) formData.append('isActive', this.ingrediant.isActive.toString());

    if (this.ingrediantId === 0) {
      this.ingrediantServices.AddIngrediant(formData).subscribe({
        next: () => this.router.navigate(['/AdminNavigation/Ingrediants']),
        error: (err) => console.error(err)
      });
    } else {
      this.ingrediantServices.UpdateIngrediant(formData, this.ingrediantId!).subscribe({
        next: () => this.router.navigate(['/AdminNavigation/Ingrediants']),
        error: (err) => console.error(err)
      });
    }
  }
}
