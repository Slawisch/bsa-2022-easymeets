import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { removeExcessiveSpaces } from '@core/helpers/string-helper';
import { IExternalAnswers } from '@core/models/IExternalAnswers';
import { userNameRegex } from '@shared/constants/model-validation';

@Component({
    selector: 'app-external-booking-confirm-page',
    templateUrl: './external-booking-confirm-page.component.html',
    styleUrls: ['./external-booking-confirm-page.component.sass'],
})
export class ExternalBookingConfirmPageComponent implements OnInit {
    @Output() confirmBooking = new EventEmitter<IExternalAnswers>();

    @Output() cancelBooking = new EventEmitter<boolean>();

    ngOnInit(): void {
        this.externalAnswers = {
            externalName: '',
            externalEmail: '',
            externalExtraInfo: '',
        };
    }

    public confirmForm = new FormGroup({
        characters: new FormControl('', [
            Validators.pattern(userNameRegex),
            Validators.minLength(2),
            Validators.maxLength(50),
        ]),
        email: new FormControl('', [Validators.email]),
        extraInfo: new FormControl('', [Validators.minLength(3), Validators.maxLength(80)]),
    });

    get characters() {
        return this.confirmForm.get('characters');
    }

    get email() {
        return this.confirmForm.get('email');
    }

    get extraInfo() {
        return this.confirmForm.get('extraInfo');
    }

    OnConfirmBooking() {
        this.confirmBooking.emit(this.externalAnswers);
    }

    public externalAnswers: IExternalAnswers;

    public userNameChanged(value: string) {
        this.confirmForm.patchValue({ characters: removeExcessiveSpaces(value) });
    }
}
