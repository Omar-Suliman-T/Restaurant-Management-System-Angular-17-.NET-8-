import { Component, OnInit } from '@angular/core';
import { MealService } from '../../services/meal.service';
import { meal } from '../../interFaces/Meals';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CategoriesService } from '../../services/categories.service';
import { category } from '../../interFaces/Categories';
import { ConfirmDialogComponent } from "../shared/confirm-dialog/confirm-dialog.component";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faPlusCircle, faEdit, faTrashAlt, faCamera, faInfoCircle } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-meal',
  standalone: true,
  imports: [CommonModule, RouterModule, ConfirmDialogComponent, FontAwesomeModule],
  templateUrl: './meal.component.html',
  styleUrls: ['../shared/shared-css/shared-table.component.css', './meal.component.css']
})
export class MealComponent implements OnInit {
  faPlusCircle = faPlusCircle;
  faEdit = faEdit;
  faTrashAlt = faTrashAlt;
  faCamera = faCamera;
  faInfoCircle = faInfoCircle;

  Meals: meal[] = [];
  categories: category[] = [];
  showConfirm: boolean = false;
  mealId: number = 0;

  constructor(
    private MealServices: MealService,
    private categoryServices: CategoriesService
  ) {}

  ngOnInit(): void {
    this.loadCategories();
    this.loadMeals();
  }

  loadCategories(): void {
    this.categoryServices.getCategories().subscribe({
      next: (response) => {
        this.categories = response;
      },
      error: (error) => {
        console.log(error);
      }
    });
  }

  loadMeals(): void {
    this.MealServices.GetMeals().subscribe({
      next: (theMeals) => {
        this.Meals = theMeals;
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  DeleteMeal(Id: number): void {
    this.MealServices.DeleteMeal(Id).subscribe({
      next: (response) => {
        this.Meals = this.Meals.filter(meal => meal.id != Id);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  onConfirm(confirmed: boolean): void {
    this.showConfirm = false;
    if (confirmed) {
      this.DeleteMeal(this.mealId);
    }
  }
}
