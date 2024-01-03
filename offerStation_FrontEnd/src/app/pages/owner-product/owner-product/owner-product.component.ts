import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ImageService } from 'src/app/services/image.service';
import { OwnerService } from 'src/app/services/owner/owner.service';
import { OwnerDetails } from 'src/app/sharedClassesAndTypes/OwnerDetails';

@Component({
  selector: 'app-owner-product',
  templateUrl: './owner-product.component.html',
  styleUrls: ['./owner-product.component.css']
})
export class OwnerProductComponent implements OnInit{
ownerInfo?:any;
id:any
errorMessage: any;
constructor(private owner:OwnerService,private activatedroute:ActivatedRoute,private Imageservice:ImageService){}
  ngOnInit(): void {
    this.activatedroute.paramMap.subscribe(paramMap=>
      {
         this.id=Number(paramMap.get('id'));
      
      });
    this.owner.GetOwnerInfo(this.id).subscribe({
      next: (data: any) => {
        console.log(data);
        this.ownerInfo = data.data;
        this.ownerInfo.image=this.Imageservice.base64ArrayToImage(this.ownerInfo.image)
      },
      error: (error: any) => this.errorMessage = error,
    })
  }


}
