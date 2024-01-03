import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ImageService } from 'src/app/services/image.service';
import { SupplierService } from 'src/app/services/supplier/supplier.service';

@Component({
  selector: 'app-suppliermainpage',
  templateUrl: './suppliermainpage.component.html',
  styleUrls: ['./suppliermainpage.component.css']
})
export class SuppliermainpageComponent implements OnInit{
  supplierinfo:any;
  id:any;
  errorMessage: any;
  constructor(private supplier:SupplierService,private activatedroute:ActivatedRoute,private imageService:ImageService){}
  ngOnInit(): void {
    this.activatedroute.paramMap.subscribe(paramMap => {
      this.id = Number(paramMap.get('id'));

    });
    this.supplier.GetSupplierInfo(this.id).subscribe({
      next: (data: any) => {
        console.log(data);
        this.supplierinfo = data.data;
        this.supplierinfo.image=this.imageService.base64ArrayToImage( this.supplierinfo.image)
      },
      error: (error: any) => this.errorMessage = error,
    })
  }

}
