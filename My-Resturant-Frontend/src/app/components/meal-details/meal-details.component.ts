import { Component, OnInit } from '@angular/core';
import { addItemToMeal, theMealDetails } from '../../interFaces/Meals';
import { MealService } from '../../services/meal.service';
import { ActivatedRoute, Router } from '@angular/router';
import { item } from '../../interFaces/Item';
import { ItemService } from '../../services/item.service';
import { NgFor, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ConfirmDialogComponent } from "../shared/confirm-dialog/confirm-dialog.component";


@Component({
  selector: 'app-meal-details',
  standalone: true,
  imports: [NgFor, FormsModule, NgIf, RouterModule, ConfirmDialogComponent],
  templateUrl: './meal-details.component.html',
  styleUrls: ['./meal-details.component.css']
})
export class MealDetailsComponent implements OnInit {
  constructor(
    private mealServices: MealService,
    private route: ActivatedRoute,
    private router: Router,
    private itemServices: ItemService
  ) {}

  mealDetials: theMealDetails[] = [];
  addItemOrChangeQuantity!: addItemToMeal;
  mealId!: number;
  itemId!: number;
  items: item[] = [];
  editingItemId: number | null = null;
  showForm2: boolean = false;
  quantity!: number;
  showConfirm: boolean = false;

  ngOnInit(): void {
    this.mealId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadMealDetails();
    this.loadItems();
  }

  loadMealDetails(): void {
    this.mealServices.GetMealItems(this.mealId).subscribe({
      next: (response) => {
        this.mealDetials = response;
      },
      error: (error) => {
        console.log(error);
      }
    });
  }

  loadItems(): void {
    this.itemServices.GetItems().subscribe({
      next: (theItems) => {
        this.items = theItems;
      },
      error: (error) => {
        console.log(error);
      }
    });
  }

  DeleteItemFromMeal(itemId: number): void {
    this.mealServices.DeleteItemFromMeal(this.mealId, itemId).subscribe({
      next: (response) => {
        console.log(response);
        this.loadMealDetails();
      },
      error: (error) => {
        console.log(error);
      }
    });
  }

  AddItemToMealOrChangeQuantity(MealId: number, ItemId: number, Quantity: number): void {
    this.addItemOrChangeQuantity = {
      itemId: ItemId,
      mealId: MealId,
      quantity: Quantity
    };

    this.mealServices.AddItemToMealOrChangeQuantity(this.addItemOrChangeQuantity).subscribe({
      next: (response) => {
        console.log(response);
        this.showForm2 = false;
        this.editingItemId = null;
        this.loadMealDetails();
      },
      error: (error) => {
        console.log(error);
      }
    });
  }

  onConfirm(confirmed: boolean): void {
    this.showConfirm = false;
    if (confirmed) {
      this.DeleteItemFromMeal(this.itemId);
    }
  }

goBackToMeals() {
  this.router.navigate(['/AdminNavigation/Meals']);
}
}
