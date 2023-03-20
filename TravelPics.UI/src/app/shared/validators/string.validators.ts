import { AbstractControl, ValidatorFn } from '@angular/forms';
import { isValidEmail } from '../string-extensions';

export class StringValidators {
  public static whiteSpaceValidator(
    control: AbstractControl
  ): { [key: string]: any } | null {
    if (!control.value) return null;

    if (control.value.trim().length == 0) {
      return { WhiteSpaceNotAllowed: 'Value does not allow empty spaces' };
    }

    return null;
  }
  public static emailValidator(
    control: AbstractControl
  ): { [key: string]: any } | null {
    if (!control.value) return null;

    if (!isValidEmail(control.value.trim())) {
      return { emailFormatError: 'Invalid email address' };
    }

    return null;
  }

  public static minLength(length: number): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      if (!control.value) return null;

      if (control.value.trim().length < length && length > 0) {
        return {
          minlength: `Value must be at least ${length} character without left/right whitespaces`,
        };
      }

      return null;
    };
  }
}
