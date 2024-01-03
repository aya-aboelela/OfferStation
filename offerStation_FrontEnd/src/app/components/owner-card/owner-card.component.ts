import { Component, Input, OnInit } from '@angular/core';
import { ImageService } from 'src/app/services/image.service';

@Component({
  selector: 'app-owner-card',
  templateUrl: './owner-card.component.html',
  styleUrls: ['./owner-card.component.css']
})
export class OwnerCardComponent implements OnInit {
  
  @Input() Type:any;
  @Input() OwnerName:any;
  @Input() OwnerRate:any;
  @Input() OwnerImge:any;
  @Input() sellerId:any
  
  constructor(private imageService:ImageService){

  }
   ngOnInit(): void {
     this.OwnerImge=this.imageService.base64ArrayToImage(this.OwnerImge)
   }
}
