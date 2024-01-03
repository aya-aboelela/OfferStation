import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CategoryService } from 'src/app/services/Category/category.service';
import { ImageService } from 'src/app/services/image.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  categoryList: any;
  errorMessage: any;
 
  //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
  //Add 'implements OnInit' to the class.
  
  constructor(private ownerCategory:CategoryService,private route:ActivatedRoute ,private imageservice:ImageService){
  }
  
  ngOnInit(): void {

    this.ownerCategory.GetAllCategory().subscribe({
      next:data=>
      {
        this.categoryList=data.data;
       
      },
      error:error=>this.errorMessage=error

    })
  
}



}
