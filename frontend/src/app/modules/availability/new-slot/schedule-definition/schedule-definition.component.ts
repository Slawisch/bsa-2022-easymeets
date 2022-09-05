import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { getDisplayDays } from '@core/helpers/display-days-helper';
import { getPossibleTimeZones } from '@core/helpers/time-zone-helper';
import { ITimeZone } from '@core/models/ITimeZone';
import { IExclusionDate } from '@core/models/schedule/exclusion-date/IExclusionDate';
import { ISchedule } from '@core/models/schedule/ISchedule';
import {
    ExclusionDatesPickerComponent,
} from '@modules/exclusion-dates/exclusion-dates-picker/exclusion-dates-picker.component';

@Component({
    selector: 'app-schedule-definition',
    templateUrl: './schedule-definition.component.html',
    styleUrls: ['./schedule-definition.component.sass'],
})
export class ScheduleDefinitionComponent implements OnInit {
    @Input() changeEvent: EventEmitter<any> = new EventEmitter();

    @Input() schedule: ISchedule;

    @Input() disabled: boolean = false;

    displayDays: string[] = getDisplayDays();

    readonly timeZones: ITimeZone[] = getPossibleTimeZones();

    selectedTimeZone: string;

    // eslint-disable-next-line no-empty-function
    constructor(private dialog: MatDialog) {}

    changeTimeZone() {
        this.schedule.timeZone = this.getSelectedTimeZoneValue();
    }

    getSelectedTimeZoneValue() {
        return this.timeZones.find((x) => x.displayValue === this.selectedTimeZone)?.value ?? 0;
    }

    getDisplayTimeZone(value: number) {
        return this.timeZones.find((x) => x.value === value)?.displayValue ?? '';
    }

    ngOnInit(): void {
        this.selectedTimeZone = this.getDisplayTimeZone(this.schedule.timeZone);
    }

    deleteExclusionDate(index: number) {
        this.schedule.exclusionDates.splice(index, 1);
    }

    showExclusionDatesWindow() {
        this.dialog
            .open<ExclusionDatesPickerComponent, any, IExclusionDate | undefined>(ExclusionDatesPickerComponent)
            .afterClosed()
            .subscribe(newExclusionDate => {
                if (newExclusionDate) {
                    this.schedule.exclusionDates.push(newExclusionDate);
                }
            });
    }

    formatTime = (time: string) => time.substring(0, 5);
}
