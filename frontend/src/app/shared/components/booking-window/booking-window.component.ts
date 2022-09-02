import { Component, EventEmitter, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { LocationTypeMapping } from '@core/helpers/location-type-mapping';
import { INewMeetingMember } from '@core/models/INewMeetingTeamMember';
import { LocationType } from '@shared/enums/locationType';
import { IConfirmButtonOptions } from '@shared/models/confirmWindow/IConfirmButtonOptions';
import { IConfirmDialogData } from '@shared/models/confirmWindow/IConfirmDialogData';

@Component({
    selector: 'app-booking-window',
    templateUrl: './booking-window.component.html',
    styleUrls: ['./booking-window.component.sass'],
})
export class BookingWindowComponent {
    maxVisibleMembers: number = 10;

    title: string;

    message?: string;

    titleImagePath?: string;

    buttonsOptions: IConfirmButtonOptions[];

    dateTime?: Date;

    duration?: number;

    meetingName?: string;

    participants?: INewMeetingMember[];

    location?: LocationType;

    link?: string;

    locationTypeMapping = LocationTypeMapping;

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: IConfirmDialogData,
        private dialogRef: MatDialogRef<BookingWindowComponent>,
    ) {
        this.title = data.title;
        this.message = data.message;
        this.titleImagePath = data.titleImagePath;
        this.buttonsOptions = data.buttonsOptions;
        this.dateTime = data.dateTime;
        this.duration = data.duration;
        this.meetingName = data.meetingName;
        this.participants = data.participants;
        this.location = data.location;
        this.link = data.link;
    }

    onClick(event: EventEmitter<void>) {
        event?.next();
        this.dialogRef.close();
    }
}
