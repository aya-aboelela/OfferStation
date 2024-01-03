import { AbstractControl } from "@angular/forms";

export function ConfirmPasswordValidator(control:AbstractControl)
{
  const password = control.get('Password');
  const confirmPassword = control.get('Confirm');

 if(password?.pristine || confirmPassword?.pristine)
 { 
    return null
 }
 else
 { 
    return  password?.value != confirmPassword?.value 
    ? {'misMatch':true}
    :null;
 }
}