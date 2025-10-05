import { Component, OnInit } from '@angular/core';
import { CategoriesService } from '../../services/categories.service';
import { category } from '../../interFaces/Categories';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ConfirmDialogComponent } from "../shared/confirm-dialog/confirm-dialog.component";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faPlusCircle, faEdit, faTrashAlt } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [CommonModule, RouterModule, ConfirmDialogComponent, FontAwesomeModule],
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  faPlusCircle = faPlusCircle;
  faEdit = faEdit;
  faTrashAlt = faTrashAlt;

  categories: category[] = [];
  showConfirm: boolean = false;
  categoryId: number = 0;

  constructor(private categoryServices: CategoriesService) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.categoryServices.getCategories().subscribe({
      next: (theCategories) => {
        this.categories = theCategories;
        console.log('Raw categories data:', theCategories);
      },
      error: (err) => {
        console.error('Error loading categories:', err);
      }
    });
  }

  getCategoryClass(categoryType: string): string {
    switch(categoryType) {
      case "Fast Food": return 'category-fastfood';
      case "Italian": return 'category-italian';
      case "Healthy": return 'category-healthy';
      default: return 'category-other';
    }
  }

  deleteCategory(id: number): void {
    this.categoryServices.DeleteCategory(id).subscribe({
      next: (response) => {
        this.categories = this.categories.filter(category => category.id !== id);
      },
      error: (err) => {
        console.error('Error deleting category:', err);
      }
    });
  }

  onConfirm(confirmed: boolean): void {
    this.showConfirm = false;
    if (confirmed) {
      this.deleteCategory(this.categoryId);
    }
  }
}
