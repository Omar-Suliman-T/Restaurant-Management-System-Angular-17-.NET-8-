import { Component, OnInit, Pipe } from '@angular/core';
import { RouterLink } from "@angular/router";
import { ItemService } from '../../services/item.service';
import { cartItem, item } from '../../interFaces/Item';
import { CartService } from '../../services/cart.service';
import { FormsModule } from "@angular/forms";
import { NgFor, NgClass, CommonModule } from '@angular/common';
import { pipe } from 'rxjs';
import { CodeService } from '../../services/code.service';

@Component({
  selector: 'app-customer-home-page',
  standalone: true,
  imports: [RouterLink, FormsModule, NgFor, NgClass,CommonModule],
  templateUrl: './customer-home-page.component.html',
  styleUrl: './customer-home-page.component.css'
})
export class CustomerHomePageComponent implements OnInit {
  constructor(private itemServices:ItemService, private cartServices:CartService,private codeServices:CodeService){}

 popularItems:cartItem[]=[];
 show:boolean=true;
 theItemId:number|null=null;
 discountCode:string|null=null;

ngOnInit() {

  this.loadDiscountCode();
    setTimeout(() => {
      document.querySelector('.customer-home')?.classList.add('loaded');
    }, 100);

      this.itemServices.GetMostPopularItems().subscribe({
        next:(response)=>{
          this.popularItems=response;
        },
        error:(err)=>{
          console.log(err);
        }
      })
  }

loadDiscountCode(){
    this.codeServices.GetDiscountCode().subscribe({
      next:(theCode)=>{
        this.discountCode=theCode;
        console.log(theCode)
      },error:(err)=>{
        console.log(err);
      }
    })
  }

AddToCart(item:cartItem){
  item.quantity=1;
  this.cartServices.addItemToCart(item);
  this.theItemId=item.id;
  setTimeout(() => {
    this.theItemId=null;
  }, 1500);
}

}

