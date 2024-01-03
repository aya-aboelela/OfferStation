import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Base } from 'src/app/sharedClassesAndTypes/Base';

@Component({
  selector: 'app-owners-index',
  templateUrl: './owners-index.component.html',
  styleUrls: ['./owners-index.component.css']

})
export class OwnersIndexComponent implements OnInit {
  catName: any="Category";
  private apiURL = Base.apiUrl + 'Owner';
  constructor(private activatedroute: ActivatedRoute, private http: HttpClient) {
  }
  
  ngOnInit(): void {
    this.activatedroute.paramMap.subscribe(paramMap => {
      this.catName = paramMap.get('category');
      console.log("hello"+this.catName);

    })

  }
}
