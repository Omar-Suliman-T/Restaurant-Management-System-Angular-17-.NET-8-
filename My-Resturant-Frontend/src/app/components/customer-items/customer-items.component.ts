import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { trigger, transition, style, animate, stagger, query } from '@angular/animations';
import { ItemService } from '../../services/item.service';
import { cartItem, item } from '../../interFaces/Item';
import { CartService } from '../../services/cart.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModel } from '@angular/forms';

@Component({
  selector: 'app-customer-items',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './customer-items.component.html',
  styleUrls: ['./customer-items.component.css'],
  animations: [
    trigger('cardAnimation', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(20px)' }),
        animate('0.5s ease', style({ opacity: 1, transform: 'translateY(0)' }))
      ])
    ])
  ]
})
export class CustomerItemsComponent implements OnInit {
  items: item[] = [];
  theButtonId: number = 0;
  expandedDescriptions: { [key: number]: boolean } = {};
  cartCount: number = 0;

  constructor(
    private itemsServices: ItemService,
    private cartServices: CartService
  ) {}

  ngOnInit(): void {
    this.cartCount=0;
    this.itemsServices.GetItems().subscribe({
      next: (response) => {
        this.items = response;
      },
      error: (error) => {
        console.log(error);
      }
    });
    this.cartServices.cartCount$.subscribe({
      next:(count)=>{
        this.cartCount=count;
      }
    })
  }

  addItemToCart(item: item) {
    const cartItem: cartItem = {
    id: item.id,
    description:"",
    name: item.name,
    image: item.image,
    price: item.price,
    quantity: 1
    }
    this.cartServices.addItemToCart(cartItem);
    this.theButtonId = item.id;

    setTimeout(() => {
      this.theButtonId = 0;
    }, 1500);
  }

  toggleDescription(itemId: number) {
    this.expandedDescriptions[itemId] = !this.expandedDescriptions[itemId];
  }
}
