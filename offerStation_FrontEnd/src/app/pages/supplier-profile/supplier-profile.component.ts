import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';

@Component({
  selector: 'app-supplier-profile',
  templateUrl: './supplier-profile.component.html',
  styleUrls: ['./supplier-profile.component.css']
})
export class SupplierProfileComponent implements OnInit {
  supplierId:number=0

  userdata:any
  constructor(private authenticationservice: AuthenticationService
    ) 
      {}
  ngOnInit(): void {
    this.authenticationservice.userData.subscribe({
      next: data => {
        console.log(data);
        
        this.userdata = data;
        this.supplierId = this.userdata ? this.userdata['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] : ''
      },
      error: error => console.log(error)
    })
    
    }


}


