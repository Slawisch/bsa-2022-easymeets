import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { BaseComponent } from '@core/base/base.component';
import { TimeRangeValidator } from '@core/helpers/time-helper';
import { hourMinutesRegex } from '@shared/constants/model-validation';

@Component({
    selector: 'app-exclusion-dates-picker',
    templateUrl: './exclusion-dates-picker.component.html',
    styleUrls: ['./exclusion-dates-picker.component.sass'],
})
export class ExclusionDatesPickerComponent extends BaseComponent implements OnInit {
    selected: Date | null;

    timeIndexIdSeed = 1;

    timeControlsIdentifiers: number[] = [0];

    formGroup: FormGroup;

    readonly startRangeIdentifier = '0';

    readonly endRangeIdentifier = '1';

    constructor(private dialogRef: MatDialogRef<ExclusionDatesPickerComponent>) {
        super();
    }

    ngOnInit() {
        this.formGroup = this.getDefaultFormGroup();
    }

    addTimeItem() {
        const [firstTimeControl, secondTimeControl] = this.getTimeFormControls();

        this.formGroup.addControl(this.timeIndexIdSeed.toString() + this.startRangeIdentifier, firstTimeControl);
        this.formGroup.addControl(this.timeIndexIdSeed.toString() + this.endRangeIdentifier, secondTimeControl);
        this.timeControlsIdentifiers = [...this.timeControlsIdentifiers, this.timeIndexIdSeed];
        this.timeIndexIdSeed++;
    }

    removeTimeItem(controlsIdentifier: number) {
        this.formGroup.removeControl(controlsIdentifier.toString() + this.startRangeIdentifier);
        this.formGroup.removeControl(controlsIdentifier.toString() + this.endRangeIdentifier);
        this.timeControlsIdentifiers.splice(this.timeControlsIdentifiers.indexOf(controlsIdentifier), 1);
    }

    clickApply() {
        if (!this.selected) {
            this.dialogRef.close();

            return;
        }
        this.dialogRef.close({
            selectedDate: this.selected,
            dayTimeRanges: this.getValidDayTimeRanges(),
        });
    }

    clickCancel() {
        this.dialogRef.close();
    }

    getValidDayTimeRanges = () =>
        this.timeControlsIdentifiers
            .map((number) => ({
                start: this.formGroup.get(number.toString() + this.startRangeIdentifier)?.value as string | null,
                end: this.formGroup.get(number.toString() + this.endRangeIdentifier)?.value as string | null,
            }))
            .filter((range) => range.start && range.end);

    private getDefaultFormGroup = () => {
        const [firstTimeControl, secondTimeControl] = this.getTimeFormControls();

        return new FormGroup({
            [`0${this.startRangeIdentifier}`]: firstTimeControl,
            [`0${this.endRangeIdentifier}`]: secondTimeControl,
        });
    };

    private getTimeFormControls = () => {
        const firstControl = new FormControl(null, {
            validators: [Validators.pattern(hourMinutesRegex)],
            updateOn: 'change',
        });

        return [
            firstControl,
            new FormControl(null, {
                validators: [Validators.pattern(hourMinutesRegex), TimeRangeValidator(firstControl)],
                updateOn: 'change',
            }),
        ];
    };
}
