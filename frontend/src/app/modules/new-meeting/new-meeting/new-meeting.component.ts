import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSelect } from '@angular/material/select';
import { BaseComponent } from '@core/base/base.component';
import { getDisplayDate } from '@core/helpers/date-helper';
import { getDisplayDuration } from '@core/helpers/display-duration-hepler';
import { IDuration } from '@core/models/IDuration';
import { INewMeeting } from '@core/models/INewMeeting';
import { INewMeetingMember } from '@core/models/INewMeetingTeamMember';
import { NewMeetingService } from '@core/services/new-meeting.service';
import { NotificationService } from '@core/services/notification.service';
import { naturalNumberRegex, newMeetingNameRegex } from '@shared/constants/model-validation';
import { LocationType } from '@shared/enums/locationType';
import { UnitOfTime } from '@shared/enums/unitOfTime';
import { map, Observable, startWith } from 'rxjs';

@Component({
    selector: 'app-new-meeting',
    templateUrl: './new-meeting.component.html',
    styleUrls: ['./new-meeting.component.sass'],
})
export class NewMeetingComponent extends BaseComponent implements OnInit {
    constructor(private newMeetingService: NewMeetingService, public notificationService: NotificationService) {
        super();
        this.getTeamMembersOfCurrentUser();
    }

    @ViewChild('singleSelect', { static: true }) singleSelect: MatSelect;

    public teamMembers: INewMeetingMember[];

    public addedMembers: INewMeetingMember[] = [];

    public filteredOptions: Observable<INewMeetingMember[]>;

    public durations: IDuration[] = getDisplayDuration();

    public dates = getDisplayDate();

    public locations = Object.keys(LocationType).filter(key => Number.isNaN(Number(key)));

    public unitOfTime = Object.keys(UnitOfTime).filter(key => Number.isNaN(Number(key)));

    public duration: number;

    public customTimeShown: boolean = false;

    public mainContentCustomTimeShown: boolean = false;

    public meetingForm: FormGroup;

    public memberFilterCtrl: FormControl = new FormControl();

    public meetingNameControl: FormControl = new FormControl('', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50),
        Validators.pattern(newMeetingNameRegex),
    ]);

    public customTimeControl: FormControl = new FormControl('', [
        Validators.pattern(naturalNumberRegex),
    ]);

    public mainContainerCustomTimeControl: FormControl = new FormControl('', [
        Validators.pattern(naturalNumberRegex),
    ]);

    public setValidation() {
        if (this.meetingForm.value.duration.time === 'Custom') {
            this.meetingForm.controls['customTime'].setValidators(Validators.required);
        } else {
            this.meetingForm.controls['customTime'].clearValidators();
        }
    }

    ngOnInit(): void {
        this.meetingForm = new FormGroup({
            meetingName: this.meetingNameControl,
            customTime: this.customTimeControl,
            unitOfTime: new FormControl(),
            location: new FormControl(),
            duration: new FormControl(),
            mainContainerDuration: new FormControl(),
            mainContainerCustomTime: this.mainContainerCustomTimeControl,
            mainContainerUnitOfTime: new FormControl(),
            date: new FormControl('', [Validators.required]),
            teamMember: new FormControl(),
        });

        this.patchFormValues();
        this.setValidation();
    }

    public patchFormValues() {
        this.meetingForm.patchValue({
            location: this.locations[0],
            duration: this.durations[0],
            unitOfTime: this.unitOfTime[0],
            mainContainerDuration: this.durations[0],
        });
    }

    public create(form: FormGroup) {
        if (this.meetingForm.valid) {
            const newMeeting: INewMeeting = {
                name: form.value.meetingName,
                location: form.value.location,
                duration: this.duration,
                startTime: form.value.date,
                meetingLink: form.value.meetingName,
                meetingMembers: this.addedMembers,
                createdAt: new Date(),
            };

            this.newMeetingService.saveNewMeeting(newMeeting)
                .pipe(this.untilThis)
                .subscribe(() => {
                    this.notificationService.showSuccessMessage('New meeting was created successfully.');
                    this.meetingForm.reset();
                    this.patchFormValues();
                    this.addedMembers = [];
                });
        } else {
            this.notificationService.showErrorMessage('All fiels need to be set');
        }
    }

    public getTeamMembersOfCurrentUser() {
        this.newMeetingService
            .getTeamMembersOfCurrentUser()
            .pipe(this.untilThis)
            .subscribe((resp) => {
                this.teamMembers = resp;

                this.filteredOptions = this.memberFilterCtrl.valueChanges.pipe(
                    startWith(''),
                    map(value => {
                        const name = typeof value === 'string' ? value : value?.name;

                        return name ? this._filter(name as string) : this.teamMembers.slice();
                    }),
                );
            });
    }

    private _filter(name: string): INewMeetingMember[] {
        const filterValue = name.toLowerCase();

        return this.teamMembers.filter(teamMembers => teamMembers.name.toLowerCase().includes(filterValue));
    }

    public displayMemberName(teamMember: INewMeetingMember): string {
        return teamMember && teamMember.name ? teamMember.name : '';
    }

    public showUnshowCustomDuration(form: FormGroup) {
        const durationValue = form.value.duration;
        const mainContainerDurationValue = form.value.mainContainerDuration;

        if (durationValue.time === 'Custom') {
            this.customTimeShown = true;
            this.setValidation();
        } else {
            this.customTimeShown = false;
            this.durationChanged(durationValue.time, durationValue.unitOfTime);
        }

        if (mainContainerDurationValue.time === 'Custom') {
            this.mainContentCustomTimeShown = true;
        } else {
            this.mainContentCustomTimeShown = false;
        }
    }

    public customDurationChanged(form: FormGroup) {
        const { customTime, unitOfTime } = form.value;

        this.durationChanged(customTime, unitOfTime);
    }

    public durationChanged(timeValue: string, unitOfTime: string) {
        if (unitOfTime === 'hour') {
            this.convertDuration(timeValue);
        } else {
            this.duration = parseInt(timeValue, 10);
        }
    }

    public convertDuration(timeValue: string) {
        this.duration = parseInt(timeValue, 10) * 60;
    }

    public addMemberToList(value: INewMeetingMember) {
        this.meetingForm.get('teamMember')?.setValue(value);
        if (!this.addedMembers.includes(value)) {
            this.addedMembers.push(value);
            this.memberFilterCtrl = new FormControl();
        }
    }

    public removeMemberToList(form: FormGroup) {
        this.addedMembers = this.addedMembers.filter((member) => member.id !== form.value.teamMember.id);
    }
}
