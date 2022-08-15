import { Component } from '@angular/core';
import { IUser } from '@core/models/IUser';
import { SpinnerService } from '@core/services/spinner.service';
import { UserService } from '@core/services/user.service';

@Component({
    selector: 'app-external-booking-page',
    templateUrl: './external-booking-page.component.html',
    styleUrls: ['./external-booking-page.component.sass'],
})
export class ExternalBookingPageComponent {
    public selectedUser: IUser;

    public selectedUserId: number = 10;

    // eslint-disable-next-line no-empty-function
    constructor(public spinnerService: SpinnerService, private userService: UserService) {
        this.userService.getCurrentUserById(this.selectedUserId).subscribe((user) => {
            this.selectedUser = user;
        });
    }
}