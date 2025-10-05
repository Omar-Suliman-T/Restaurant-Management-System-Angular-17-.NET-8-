// item-form.component.ts
import { Component, OnInit } from '@angular/core';
import { editItem } from '../../interFaces/Item';
import { ItemService } from '../../services/item.service';
import { CategoriesService } from '../../services/categories.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faSave, faTimes } from '@fortawesome/free-solid-svg-icons';
import { category } from '../../interFaces/Categories';

@Component({
  selector: 'app-item-form',
  standalone: true,
  imports: [FormsModule, CommonModule, FontAwesomeModule],
  templateUrl: './item-form.component.html',
  styleUrls: ['../shared/shared-css/shared-forms.css', './item-form.component.css']
})
export class ItemFormComponent implements OnInit {
  faSave = faSave;
  faTimes = faTimes;

  item: editItem = {
    name: null,
    description: null,
    price: null,
    category: null,
    image: null,
    ingrediants: [],
    stock: null,
    isActive: null
  };

  isRequaired!: boolean;
  itemIngrediants: string = "";
  itemId: number | null = null;
  selectedImage: File | null = null;
  imagePreview: string | null = null;
  categories: category[] = [];

  constructor(
    private itemService: ItemService,
    private categoriesService: CategoriesService,
    private route: ActivatedRoute,
    public router: Router
  ) {}

  ngOnInit(): void {
    this.itemId = Number(this.route.snapshot.paramMap.get('id'));
    this.isRequaired = this.itemId === 0;

    this.loadCategories();

    if (this.itemId && this.itemId !== 0) {
      this.loadItemData(this.itemId);
    }
  }

  loadCategories(): void {
    this.categoriesService.getCategories().subscribe({
      next: (categories) => {
        this.categories = categories;
      },
      error: (err) => {
        console.error('Error loading categories:', err);
      }
    });
  }

  loadItemData(id: number): void {
    this.itemService.GetItemById(id).subscribe({
      next: (item) => {
        this.item = item;
        this.itemIngrediants = item.ingrediants.join(', ');
      },
      error: (err) => {
        console.error('Error loading item:', err);
      }
    });
  }

  onImageSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.selectedImage = file;
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
  }

  UpdateItem(): void {
    if (this.isRequaired) {
      const hasNull = Object.values(this.item).some(value => value === null);
      if (hasNull || !this.itemIngrediants) {
        return;
      }
    }

    const formData = new FormData();
    if (this.selectedImage) formData.append('image', this.selectedImage);
    if (this.item.name) formData.append('name', this.item.name);
    if (this.item.description) formData.append('description', this.item.description);
    if (this.item.price) formData.append('price', this.item.price.toString());
    if (this.item.category) formData.append('category', this.item.category);
    if (this.item.stock) formData.append('stock', this.item.stock.toString());
    if (this.item.isActive !== null) formData.append('isActive', this.item.isActive.toString());

    // Process ingredients
    if (this.itemIngrediants) {
      const ingredientsArray = this.itemIngrediants.split(',').map(i => i.trim());
      ingredientsArray.forEach((ingredient, index) => {
        formData.append(`ingrediants[${index}]`, ingredient);
      });
    }

    if (this.itemId === 0) {
      this.itemService.AddItem(formData).subscribe({
        next: () => this.router.navigate(['/AdminNavigation/Items']),
        error: (err) => console.error(err)
      });
    } else {
      this.itemService.UpdateItem(formData, this.itemId!).subscribe({
        next: () => this.router.navigate(['/AdminNavigation/Items']),
        error: (err) => console.error(err)
      });
    }
  }
}
