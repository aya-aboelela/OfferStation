import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OwnerService } from 'src/app/services/owner/owner.service';

@Component({
  selector: 'app-ownerdetails',
  templateUrl: './ownerdetails.component.html',
  styleUrls: ['./ownerdetails.component.css']
})
export class OwnerdetailsComponent implements OnInit {
  AddressList:any;
  id:any;
  errorMessage: any;
  constructor(private owner:OwnerService,private activatedroute:ActivatedRoute)
  {

  }
  ngOnInit(): void {
    this.activatedroute.paramMap.subscribe(paramMap=>
      {
         this.id=Number(paramMap.get('id'));
      
      });
    this.owner.GetAddressByOwnerId(this.id).subscribe({
  
      next: (data: { data: any; }) => {
        console.log(data);
        this.AddressList = data.data
        console.log(this.AddressList);
      },
      error: (error: any) => this.errorMessage = error
    
    });
  }
  
}
