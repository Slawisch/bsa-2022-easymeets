import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import * as languageHelper from '@core/helpers/language-helper';
import * as timeHelper from '@core/helpers/time-format-label-mapping';
import { IUser } from '@core/models/IUser';
import { AuthService } from '@core/services/auth.service';
import { NotificationService } from '@core/services/notification.service';
import { SpinnerService } from '@core/services/spinner.service';
import { UserService } from '@core/services/user.service';
import { DateFormat } from '@shared/enums/dateFormat';
import firebase from 'firebase/compat';
import { finalize, Observable, switchMap, tap } from 'rxjs';

import { AuthModule } from '../auth.module';

@Injectable({
    providedIn: AuthModule,
})
export class AuthFormService {
    constructor(
        private authService: AuthService,
        private spinnerService: SpinnerService,
        private userService: UserService,
        private notificationService: NotificationService,
        private router: Router, // eslint-disable-next-line no-empty-function
    ) {}

    public signIn(email: string, password: string): Observable<IUser> {
        return this.authenticate(this.authService.signIn(email, password));
    }

    public signUp(email: string, password: string, userName: string): Observable<IUser> {
        return this.authenticate(this.authService.signUp(email, password), userName);
    }

    public onSignInWithGoogle(): Observable<IUser> {
        return this.authenticate(this.authService.loginWithGoogle());
    }

    private authenticate(authMethod: Observable<firebase.auth.UserCredential>, userName?: string) {
        this.spinnerService.show();

        return authMethod.pipe(
            finalize(() => this.spinnerService.hide()),
            switchMap((userCredential) => this.createUser(userCredential, userName)),
        );
    }

    private createUser(resp: firebase.auth.UserCredential, userName: string = 'UserName'): Observable<IUser> {
        return this.userService
            .createUser({
                uid: resp.user?.uid,
                userName: resp.user?.displayName ?? userName,
                email: resp.user?.email ?? '',
                image: resp.user?.photoURL ?? undefined,
                language: languageHelper.getLanguage(),
                timeFormat: timeHelper.getTimeFormat(),
                dateFormat: DateFormat.MonthDayYear,
                phone: resp.user?.phoneNumber ?? undefined,
                timeZone: new Date().getTimezoneOffset(),
            })
            .pipe(
                tap(() => {
                    this.notificationService.showSuccessMessage('Authentication successful');
                    this.router.navigateByUrl('availability');
                }),
            );
    }
}
