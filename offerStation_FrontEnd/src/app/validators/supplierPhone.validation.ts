import { AbstractControl, ValidatorFn } from "@angular/forms";

export function SupplierPhoneValidator(control:AbstractControl)
{

  const phone = control.get('Phone');
  const phoneRegex = /^\d{11}$/; // Regular expression to match only digits

    const isValid = phoneRegex.test(control.value);

    return isValid ? null : { invalidSupplierPhone: true };
 

}
    