import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IAvailabilitySlot } from '@core/models/IAvailabilitySlot';
import { IUser } from '@core/models/IUser';
import { SpinnerService } from '@core/services/spinner.service';

@Component({
    selector: 'app-user-slot',
    templateUrl: './user-slot.component.html',
    styleUrls: ['./user-slot.component.sass'],
})
export class UserSlotComponent {
    @Input() public userSlots: Array<IAvailabilitySlot>;

    @Input() public currentUser: IUser;

    @Output() isReload = new EventEmitter<boolean>();

    // eslint-disable-next-line no-empty-function
    constructor(public spinnerService: SpinnerService) {}

    isDeleted(isRemove: boolean) {
        this.isReload.emit(isRemove);
    }

    isChangedActivity(isChanged: boolean) {
        this.isReload.emit(isChanged);
    }
}
