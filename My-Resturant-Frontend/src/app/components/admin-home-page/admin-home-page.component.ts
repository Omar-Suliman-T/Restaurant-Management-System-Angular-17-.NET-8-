import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { trigger, transition, style, animate } from '@angular/animations';
import { CodeService } from '../../services/code.service';
import { code } from '../../interFaces/Code';
import { OrderService } from '../../services/order.service';
import { interval, Observable } from 'rxjs';
import { order } from '../../interFaces/Order';
import { userServices } from '../../services/user.service';
import { ItemService } from '../../services/item.service';

@Component({
  selector: 'app-admin-home-page',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './admin-home-page.component.html',
  styleUrls: ['./admin-home-page.component.css'],
  animations: [
    trigger('fadeInUp', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(20px)' }),
        animate('0.5s ease', style({ opacity: 1, transform: 'translateY(0)' }))
      ])
    ]),
    trigger('fadeIn',[
      transition(':enter',[
        style({opacity:0}),
        animate('0.2s ease',style({opacity:1}))
      ]),
      transition(':leave',[
        style({opacity:1}),
        animate('0.2s ease',style({opacity:0}))
      ])
    ])
  ]
})
export class AdminHomePageComponent implements OnInit {
  todaysOrders: number | null = null;
  activeUsers: number |null=null;
  availableItems: number= 0;
  discountCode:string|null=null;
  newDiscountCode: string = '';
  showInput: boolean = false;
  recentOrders:order[]=[];

  stats = [
    { title: 'Monthly Revenue', value: 0 , icon: 'fas fa-dollar-sign', color: '#2ecc71' },
    { title: 'Pending Orders', value: 0, icon: 'fas fa-clock', color: '#f39c12' },
    { title: 'New Customers', value: 0, icon: 'fas fa-users', color: '#3498db' },
    { title: 'Top Selling Item', value: 'not found', icon: 'fas fa-star', color: '#e74c3c' }
  ];


  constructor(private codeService: CodeService , private orderServices:OrderService, private userServices:userServices,private itemServices:ItemService) {
  }

  ngOnInit(): void {
    this.loadDiscountCode();
    this.OrdersFunctions();
    this.UsersFunctions();
    this.ItemsFunctions();
    interval(15000).subscribe(() => this.OrdersFunctions());
    interval(15000).subscribe(() => this.UsersFunctions());

  }

  loadDiscountCode(): void {
    this.codeService.GetDiscountCode().subscribe({
      next: (disCode) => {
        this.discountCode = disCode;
      },
      error: (err) => {
        console.error('Error loading discount code:', err);
      }
    });
  }

  AddDiscountCode(newCode: string): void {
    if (!newCode) {
      return;
    }
    let mycode:code={
      discountCode:newCode
    }
    this.codeService.UpdateDiscountCode(mycode).subscribe({
      next: (response) => {
        this.discountCode=newCode;
        this.newDiscountCode = '';
        this.showInput = false;
      },
      error: (err) => {
        console.error('Error adding discount code:', err);
      }
    });
  }

  DeleteDiscountCode(): void {
    let mycode:code={
      discountCode:null
    }
    this.codeService.UpdateDiscountCode(mycode).subscribe({
      next: (response) => {
        this.discountCode = null;
        this.showInput = false;
      },
      error: (err) => {
        console.error('Error deleting discount code:', err);
      }
    });
  }

    OrdersFunctions(){
    this.orderServices.GetOrders().subscribe({
      next:(orders)=>{
        if(orders!==null){
         this.stats[1].value= orders.filter(o=>o.orderStatus==='Confirmed').length.toString();

         const today = new Date();
         this.todaysOrders = orders.filter(o => {
         const orderDate = new Date(o.creationDate);
         return orderDate.getFullYear() === today.getFullYear() &&
              orderDate.getMonth() === today.getMonth() &&
              orderDate.getDate() === today.getDate();
          }).length;

          this.recentOrders=orders.reverse().slice(0,5);

          this.stats[0].value=((orders.reduce((sum:number , order)=> sum+=order.netPrice,0))*0.025);
            }
      },
      error:(err)=>{
        console.log(err);
      }
    })
  }

  UsersFunctions(){
    this.userServices.getUsers().subscribe({
      next:(users)=>{
        if(users){
        this.stats[2].value=users.filter(user=>{
          const today=new Date();
          const orderDate = new Date(user.creationDate);
         return orderDate.getFullYear() === today.getFullYear() &&
              orderDate.getMonth() === today.getMonth() &&
              orderDate.getDate() === today.getDate()}).length

          this.activeUsers=users.filter(u=>u.isActive).length;

          }
        },
        error:(err)=> {
          console.log(err);
        },
    })
  }

  ItemsFunctions(){
    this.itemServices.GetItems().subscribe({
      next:(items)=>{
        if(items){
        this.availableItems=items.length;
      }
    },
    error:(err)=>{
      console.log(err);
    }
    })

    this.itemServices.GetMostPopularItems().subscribe({
      next:(populatItems)=>{
        console.log(populatItems)
        if(populatItems){
        this.stats[3].value=populatItems.at(0)!.name;
        }
      },
      error:(err)=>{
        console.log(err);
      }
    })

  }



}
