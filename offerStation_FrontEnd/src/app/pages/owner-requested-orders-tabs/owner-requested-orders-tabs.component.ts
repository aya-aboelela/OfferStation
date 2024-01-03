import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-owner-requested-orders-tabs',
  templateUrl: './owner-requested-orders-tabs.component.html',
  styleUrls: ['./owner-requested-orders-tabs.component.css']
})
export class OwnerRequestedOrdersTabsComponent implements OnInit {
  ownerId!:number

  constructor(private route:ActivatedRoute) 
  {
    
  }

  ngOnInit(): void {
    this.ownerId = this.route.snapshot.params['id']



  }

}
