import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';

@Component({
  selector: 'app-customer-profile',
  templateUrl: './customer-profile.component.html',
  styleUrls: ['./customer-profile.component.css']
})
export class CustomerProfileComponent  implements OnInit{
  customerid:number=0

  userdata:any
  constructor(private authenticationservice: AuthenticationService
    ) 
      {}
  ngOnInit(): void {
    this.authenticationservice.userData.subscribe({
      next: data => {
        console.log(data);
        
        this.userdata = data;
        this.customerid = this.userdata ? this.userdata['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] : ''
      },
      error: error => console.log(error)
    })
    
    }


  

}
