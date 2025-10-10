import { Component, OnInit } from '@angular/core';
import { ItemService } from '../../services/item.service';
import { item } from '../../interFaces/Item';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CategoriesService } from '../../services/categories.service';
import { category } from '../../interFaces/Categories';
import { ConfirmDialogComponent } from "../shared/confirm-dialog/confirm-dialog.component";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faPlusCircle, faEdit, faTrashAlt, faCamera } from '@fortawesome/free-solid-svg-icons';
import { pipe } from 'rxjs';

@Component({
  selector: 'app-item',
  standalone: true,
  imports: [CommonModule, RouterModule, ConfirmDialogComponent, FontAwesomeModule],
  templateUrl: './item.component.html',
  styleUrls: ['../shared/shared-css/shared-table.component.css', './item.component.css']
})
export class ItemComponent implements OnInit {
  faPlusCircle = faPlusCircle;
  faEdit = faEdit;
  faTrashAlt = faTrashAlt;
  faCamera = faCamera;

  Items: item[] = [];
  categories: category[] = [];
  showConfirm: boolean = false;
  itemId: number = 0;

  constructor(
    private ItemServices: ItemService,
    private categoryServices: CategoriesService
  ) {}

  ngOnInit(): void {
    this.loadCategories();
    this.loadItems();
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

  loadItems(): void {
    this.ItemServices.GetItems().subscribe({
      next: (theItems) => {
        this.Items = theItems;
        console.log('items:',theItems)
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  DeleteItem(Id: number): void {
    this.ItemServices.DeleteItem(Id).subscribe({
      next: (response) => {
        this.Items = this.Items.filter(item => item.id != Id);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  onConfirm(confirmed: boolean): void {
    this.showConfirm = false;
    if (confirmed) {
      this.DeleteItem(this.itemId);
    }
  }
}
