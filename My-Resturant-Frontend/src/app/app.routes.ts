import { Routes } from '@angular/router';
import { AdmineNavigationComponent } from './components/AdmineNavigation/AdminNavigation.component';
import { UsersComponent } from './components/users/users.component';
import { provideRouter } from '@angular/router';
import { ApplicationConfig } from '@angular/core';
import { AdminHomePageComponent} from './components/admin-home-page/admin-home-page.component';
import { LogInComponent } from './components/log-in/log-in.component';
import { AdminAuthGuard } from './AdminAuthGuard';
import { UserFormComponent } from './components/user-form/user-form.component';
import { CategoriesComponent } from './components/categories/categories.component';
import { CategoryFormComponent } from './components/category-form/catefory-form.component';
import { IngrediantsComponent } from './components/ingrediants/ingrediants.component';
import { IngrediantFormComponent } from './components/ingrediant-form/ingrediant-form.component';
import { ItemComponent } from './components/item/item.component';
import { ItemFormComponent } from './components/item-form/item-form.component';
import { MealComponent } from './components/meal/meal.component';
import { MealFormComponent } from './components/meal-form/meal-form.component';
import { MealDetailsComponent } from './components/meal-details/meal-details.component';
import { OrderComponent } from './components/orders/order.component';
import { AboutUsComponent } from './components/about-us/about-us.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { single } from 'rxjs';
import { CustomerNavigationComponent } from './components/customer-navigation/customer-navigation.component';
import { CustomerItemsComponent } from './components/customer-items/customer-items.component';
import { CustomerMealsComponent } from './components/customer-meals/customer-meals.component';
import { MyCartComponent } from './components/my-cart/my-cart.component';
import { CustomerAuthGuard } from './CustomerAuthGuard';
import { lastOrderResolver } from './services/last-order.resolver';
import { OrderDetialsComponent } from './components/order-detialse/order-detialse.component';
import { CustomerHomePageComponent } from './components/customer-home-page/customer-home-page.component';
import { RestaurantStoryComponent } from './components/resturant-story/resturant-story.component';
import { ReservationsComponent } from './components/reservations/reservations.component';

export const routes: Routes = [
  {path:"",redirectTo:"LogIn",pathMatch:'full'},
  {path:"AdminNavigation",component:AdmineNavigationComponent,canActivate:[AdminAuthGuard],
    children:[
        {path:'Home',component:AdminHomePageComponent,canActivate:[AdminAuthGuard]},
        {path:'Users',component:UsersComponent,canActivate:[AdminAuthGuard]},
        {path:'EditUser/:id',component:UserFormComponent,canActivate:[AdminAuthGuard]},
        {path:'AddUser',component:UserFormComponent,canActivate:[AdminAuthGuard]},
        {path:'Categories',component:CategoriesComponent,canActivate:[AdminAuthGuard]},
        {path:'EditCategory/:id',component:CategoryFormComponent,canActivate:[AdminAuthGuard]},
        {path:'AddCategory',component:CategoryFormComponent,canActivate:[AdminAuthGuard]},
        {path:'Ingrediants',component:IngrediantsComponent,canActivate:[AdminAuthGuard]},
        {path:'EditIngrediant/:id',component:IngrediantFormComponent,canActivate:[AdminAuthGuard]},
        {path:'AddIngrediant',component:IngrediantFormComponent,canActivate:[AdminAuthGuard]},
        {path:'Items',component:ItemComponent,canActivate:[AdminAuthGuard]},
        {path:'EditItem/:id',component:ItemFormComponent,canActivate:[AdminAuthGuard]},
        {path:'AddItem',component:ItemFormComponent,canActivate:[AdminAuthGuard]},
        {path:'Meals',component:MealComponent,canActivate:[AdminAuthGuard]},
        {path:'EditMeal/:id',component:MealFormComponent,canActivate:[AdminAuthGuard]},
        {path:'AddMeal',component:MealFormComponent,canActivate:[AdminAuthGuard]},
        {path:'MealDetails/:id',component:MealDetailsComponent,canActivate:[AdminAuthGuard]},
        {path:'Orders',component:OrderComponent,canActivate:[AdminAuthGuard]},
        {path:'AboutUs',component:AboutUsComponent},
        {path:'ResturantStory',component:RestaurantStoryComponent},
        {path:'Reservations',component:ReservationsComponent,canActivate:[AdminAuthGuard]}
    ]
  },
  {path:"CustomerNavigation",component:CustomerNavigationComponent,canActivate:[CustomerAuthGuard],
    children:[
        {path:'Home',component:CustomerHomePageComponent,canActivate:[CustomerAuthGuard]},
        {path:'AboutUs',component:AboutUsComponent},
        {path:'ResturantStory',component:RestaurantStoryComponent},
        {path:'CustomerItems',component:CustomerItemsComponent,canActivate:[CustomerAuthGuard]},
        {path:'CustomerMeals',component:CustomerMealsComponent,canActivate:[CustomerAuthGuard]},
        {path:'MyCart',component:MyCartComponent,canActivate:[CustomerAuthGuard]},
        {path:'MyReservation',component:ReservationsComponent,canActivate:[CustomerAuthGuard]},
        {path:'OrderDetiales',component:OrderDetialsComponent,canActivate:[CustomerAuthGuard],
          resolve: {
          lastOrder: lastOrderResolver
        }},
    ]
  },
  {path:'LogIn',component:LogInComponent},
  {path:'SignUp',component:SignUpComponent},
  {path:'AboutUs',component:AboutUsComponent},
  {path:'ResturantStory',component:RestaurantStoryComponent},

  // {path:'MealDetails',component:MealDetailsComponent,canActivate:[AuthGuard]},
];
