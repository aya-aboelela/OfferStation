import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-supplier-requested-orders-tabs',
  templateUrl: './supplier-requested-orders-tabs.component.html',
  styleUrls: ['./supplier-requested-orders-tabs.component.css']
})
export class SupplierRequestedOrdersTabsComponent implements OnInit {
  supplierId!:number

  constructor(private route:ActivatedRoute) 
  {
    
  }

  ngOnInit(): void {
    this.supplierId = this.route.snapshot.params['id']



  }

}