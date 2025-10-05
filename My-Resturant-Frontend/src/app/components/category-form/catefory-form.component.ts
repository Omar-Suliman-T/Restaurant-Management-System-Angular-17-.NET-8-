import { Component, OnInit, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { editCategory } from '../../interFaces/Categories';
import { CategoriesService } from '../../services/categories.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faSave, faTimes } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-category-form',
  standalone: true,
  imports: [FormsModule, CommonModule, FontAwesomeModule],
  templateUrl: './catefory-form.component.html',
  styleUrls: ['./catefory-form.component.css','../shared/shared-css/shared-forms.css']
})
export class CategoryFormComponent implements OnInit {
  faSave = faSave;
  faTimes = faTimes;
  @ViewChild('userForm') userForm!: NgForm;

  categroyId: number | null = null;
  formSubmitted = false;

  category: editCategory = {
    name: null,
    description: null,
    isActive: null
  }

  constructor(
    private categoryServices: CategoriesService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.categroyId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.categroyId && this.categroyId !== 0) {
      this.loadCategoryData(this.categroyId);
    }
  }

  loadCategoryData(categoryId: number): void {
    this.categoryServices.GetCategoryById(categoryId).subscribe({
      next: (category) => {
        this.category = category;
        console.error('category:', category);

      },
      error: (err) => {
        console.error('Error loading category data:', err);
      }
    });
  }

  showError(fieldName: string): boolean {
    const field = this.userForm?.controls[fieldName];
    return (this.formSubmitted && field?.invalid) || false;
  }

  UpdateCategory(): void {
    this.formSubmitted = true;

    if (this.userForm.valid) {
      if (this.categroyId && this.categroyId !== 0) {
        this.updateCategory();
      } else {
        this.addCategory();
      }
    }
  }

  addCategory(): void {
    this.categoryServices.AddCategory(this.category).subscribe({
      next: (response) => {
        this.router.navigate(['/AdminNavigation/Categories']);
      },
      error: (error) => {
        console.error('Error adding category:', error);
      }
    });
  }

  updateCategory(): void {
    this.categoryServices.UpdateCategory(this.category, this.categroyId!).subscribe({
      next: (response) => {
        this.router.navigate(['/AdminNavigation/Categories']);
      },
      error: (error) => {
        console.error('Error updating category:', error);
      }
    });
  }

  cancel(): void {
    this.router.navigate(['/AdminNavigation/Categories']);
  }
}
