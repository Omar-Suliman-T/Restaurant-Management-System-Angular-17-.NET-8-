import { Directive, ElementRef, Input, input } from '@angular/core';

@Directive({
  selector: '[appHighlight]',
  standalone: true
})
export class HighlightDirective {
  constructor(private el:ElementRef) {
   }
   @Input() mycolor="";

   
public heigtlight(color:string){
  this.el.nativeElement.backgroundcolor=color;
}
}
