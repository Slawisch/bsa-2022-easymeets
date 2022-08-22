import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '@core/services/auth.service';
import { UserService } from '@core/services/user.service';
import { EmailValidator } from '@modules/auth/validators/email-validator';
import firebase from 'firebase/compat';

@Component({
    selector: 'app-sign-in-form',
    templateUrl: './sign-in-form.component.html',
    styleUrls: ['./sign-in-form.component.sass', '../../shared-styles.sass'],
})
export class SignInFormComponent {
    public hidePassword = true;

    public signInForm = new FormGroup(
        {
            email: new FormControl(
                '',
                [Validators.required, Validators.email],
                [EmailValidator.loginEmailValidator(this.authService)],
            ),
            password: new FormControl('', [Validators.required, Validators.minLength(8)]),
        },
        {
            updateOn: 'submit',
        },
    );

    // eslint-disable-next-line no-empty-function
    constructor(private authService: AuthService, private userService: UserService, private router: Router) {}

    private setCredentialsIncorrect() {
        this.signInForm.get('password')?.setErrors({ incorrectCredentials: true });
    }

    private handleAuthenticationResponse(resp: firebase.auth.UserCredential | void): void {
        if (resp) {
            this.userService.getCurrentUser();
            this.router.navigateByUrl('availability');
        } else {
            this.setCredentialsIncorrect();
        }
    }

    public onSignIn(): void {
        if (this.signInForm.valid) {
            this.authService
                .signIn(this.signInForm.value.email!, this.signInForm.value.password!)
                .then((resp) => this.handleAuthenticationResponse(resp));
        }
    }

    public onSignInWithGoogle(): void {
        this.authService.loginWithGoogle().then((resp) => this.handleAuthenticationResponse(resp));
    }
}
