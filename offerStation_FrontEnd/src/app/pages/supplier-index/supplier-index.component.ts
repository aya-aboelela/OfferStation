import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Base } from 'src/app/sharedClassesAndTypes/Base';

@Component({
  selector: 'app-supplier-index',
  templateUrl: './supplier-index.component.html',
  styleUrls: ['./supplier-index.component.css']
})
export class SupplierIndexComponent  implements OnInit{
  catName: any="Category";
  private apiURL = Base.apiUrl + 'Owner';
  constructor(private activatedroute: ActivatedRoute, private http: HttpClient) {
  }
  
  ngOnInit(): void {
    this.activatedroute.paramMap.subscribe(paramMap => {
      this.catName = paramMap.get('category');

    })

  }

}
