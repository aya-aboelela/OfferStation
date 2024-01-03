import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SupplierService } from 'src/app/services/supplier/supplier.service';

@Component({
  selector: 'app-supplieraddresses',
  templateUrl: './supplieraddresses.component.html',
  styleUrls: ['./supplieraddresses.component.css']
})
export class SupplieraddressesComponent  implements OnInit{
  AddressList:any;
  id:any;
  errorMessage: any;
  constructor(private supplier:SupplierService,private activatedroute:ActivatedRoute)
  {

  }
  ngOnInit(): void {
    this.activatedroute.paramMap.subscribe(paramMap => {
      this.id = Number(paramMap.get('id'));

    });
    this.supplier.GetAddressBySupplierId(this.id).subscribe({
  
      next: (data: { data: any; }) => {
        console.log(data);
        this.AddressList = data.data
        console.log(this.AddressList);
      },
      error: (error: any) => this.errorMessage = error
    
    });
  }
}
