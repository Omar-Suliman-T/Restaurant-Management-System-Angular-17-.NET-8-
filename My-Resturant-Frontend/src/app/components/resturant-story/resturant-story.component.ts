import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-restaurant-story',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './resturant-story.component.html',
  styleUrls: ['./resturant-story.component.css']
})
export class RestaurantStoryComponent {
  isExpanded: boolean = false;

  toggleExpand(): void {
    this.isExpanded = !this.isExpanded;

    // Scroll to the expanded content if opening
    if (this.isExpanded) {
      setTimeout(() => {
        const element = document.querySelector('.expandable-content');
        if (element) {
          element.scrollIntoView({ behavior: 'smooth', block: 'start' });
        }
      }, 300);
    }
  }
}
