import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  userdata:any;
  userName:any;
  UserRole:any
  constructor(private fb: FormBuilder, private _AuthenticationService: AuthenticationService, private router: Router) {
    this._AuthenticationService.userData.subscribe({
      next: data => {
        console.log(data);
        
        this.userdata = data;
        this.userName = this.userdata ? this.userdata['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] : ''
        this.UserRole= this.userdata? this.userdata['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] : ''
     

      },
      error: error => console.log(error)
    })
  }
  msg = "";
  loginForm = this.fb.group({
    password: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email]],
  })

  get password() {
    return this.loginForm.get('password');
  }
  get email() {
    return this.loginForm.get('email');
  }
  error: string = ''

  submitData() {    
      this._AuthenticationService.login(this.loginForm.value).subscribe({
      next:data=>{        
        if (data.success == true) {
          localStorage.setItem("userToken",data.data.token);
          this._AuthenticationService.saveUserData()
          if(this.UserRole=="owner")
          { 
            this.router.navigate(['supplierHome']);

          }else if(this.UserRole=="customer")
          {
            this.router.navigate(['']);

          }
          else if(this.UserRole=="supplier")
          {
            this.router.navigate(['']);
          }
          else if(this.UserRole="admin")
          {
            this.router.navigate(['admin']);
          }
        
          //  this.router.navigate([''])
      
        }
        else {
          this.msg = data.message;
        }
      },
      error:error=>console.log(error)
    });
  }
  removemesage(){
    this.msg="";
  }
}
