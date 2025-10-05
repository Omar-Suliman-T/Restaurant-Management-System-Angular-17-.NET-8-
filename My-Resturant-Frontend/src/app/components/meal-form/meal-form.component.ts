import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { MealDetails, editMeal, meal } from '../../interFaces/Meals';
import { MealService } from '../../services/meal.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ItemService } from '../../services/item.service';
import { item } from '../../interFaces/Item';
import { CommonModule, NgFor, NgIf } from '@angular/common';
import { CategoriesService } from '../../services/categories.service';
import { category } from '../../interFaces/Categories';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faSave, faTimes } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-meal-form',
  standalone: true,
  imports: [FormsModule, NgFor, NgIf, CommonModule, FontAwesomeModule],
  templateUrl: './meal-form.component.html',
  styleUrls: ['../shared/shared-css/shared-forms.css', './meal-form.component.css']
})
export class MealFormComponent implements OnInit {
  faSave = faSave;
  faTimes = faTimes;

 meal: editMeal = {
    name: null,
    description: null,
    category: null,
    image: null,
    stock: null,
    isActive: null,
    details: []
  };
  categories: category[] = [];
  items: item[] = [];
  nothinInside: boolean = false;
  showForm: boolean = false;
  isRequaired: boolean = false;
  mealId: number | null = null;
  selectedImage: File | null = null;
  imagePreview: string | null = null;

  constructor(
    private MealServices: MealService,
    private route: ActivatedRoute,
    public router: Router,
    private itemServices: ItemService,
    private categoryServices: CategoriesService
  ) {}

  ngOnInit(): void {
    this.categoryServices.getCategories().subscribe({
      next: (response) => {
        this.categories = response;
      },
      error: (error) => {
        console.log(error);
      }
    });

    this.itemServices.GetItems().subscribe({
      next: (theItems) => {
        this.items = theItems;
      },
      error: (error) => {
        console.log(error);
      }
    });

    this.mealId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.mealId == 0) {
      this.isRequaired = true;
      this.showForm = true;
    } else {
      this.loadMealData(this.mealId);
    }
  }

  loadMealData(id: number): void {
    this.MealServices.GetMealById(id).subscribe({
      next: (meal) => {
        this.meal = meal;
        // Initialize details array if needed
        if (!this.meal.details || this.meal.details.length < 5) {
          const emptyDetails = Array(5 - (this.meal.details?.length || 0)).fill({
            itemID: null,
            quantity: null
          });
          this.meal.details = [...(this.meal.details || []), ...emptyDetails];
        }
      },
      error: (error) => {
        console.log(error);
      }
    });
  }

  onImageSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.selectedImage = file;
      this.meal.image = this.selectedImage;
      const reader = new FileReader();
      reader.onload = (e) => {
        this.imagePreview = e.target?.result as string;
      };
      reader.readAsDataURL(file);
    }
  }

  removeImage(): void {
    this.selectedImage = null;
    this.imagePreview = null;
    this.meal.image = null;
  }

  UpdateMeal(): void {
    this.nothinInside = this.meal.details.filter(d => d.itemID !== null && d.quantity !== null).length === 0;

    if (this.isRequaired) {
      const hasNull = Object.entries(this.meal).some(([key, value]) =>
        key !== 'details' && value === null
      );

      if (hasNull || this.nothinInside) {
        return;
      }
    }

    const validDetails = this.meal.details.filter(d => d.itemID !== null && d.quantity !== null);
    const formData = new FormData();

    if (this.selectedImage !== null) {
      formData.append('image', this.selectedImage);
    }
    if (this.meal.name !== null) {
      formData.append('name', this.meal.name);
    }
    if (this.meal.category !== null) {
      formData.append('category', this.meal.category.toString());
    }
    if (this.meal.isActive !== null) {
      formData.append('isActive', this.meal.isActive.toString());
    }
    if (this.meal.description !== null) {
      formData.append('description', this.meal.description);
    }
    if (this.meal.stock !== null) {
      formData.append('stock', this.meal.stock.toString());
    }
    if (validDetails.length !== 0) {
      formData.append('details', JSON.stringify(validDetails));
    }

    if (this.mealId == 0) {
      this.MealServices.AddMeal(formData).subscribe({
        next: (res) => {
          this.router.navigate(['/AdminNavigation/Meals']);
        },
        error: (err) => console.error(err)
      });
    } else {
      this.MealServices.UpdateMeal(formData, this.mealId).subscribe({
        next: (res) => {
          this.router.navigate(['/AdminNavigation/Meals']);
        },
        error: (err) => console.error(err)
      });
    }
  }
}
