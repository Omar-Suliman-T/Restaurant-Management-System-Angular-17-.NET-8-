import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { OrderService } from './order.service';
import { of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { order } from '../interFaces/Order';

export const lastOrderResolver: ResolveFn<order | null> = () => {
  const orderService = inject(OrderService);

  return orderService.GetLastOrderDetials().pipe(
    catchError(error => {
      console.error('Error resolving last order:', error);
      return of(null);
    })
  );
};
