import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { getNewAvailabilityMenu } from '@core/helpers/new-availability-menu-helper';
import { SideMenuGroupTabs } from '@core/interfaces/sideMenu/tabs/sideMenuGroupTabs';
import { IAvailabilitySlot } from '@core/models/IAvailiabilitySlot';
import { EventDetailComponent } from '@modules/availability/new-slot/event-detail/event-detail.component';
import { GeneralComponent } from '@modules/availability/new-slot/general/general.component';
import { ScheduleComponent } from '@modules/availability/new-slot/schedule/schedule.component';

@Component({
    selector: 'app-new-availability',
    templateUrl: './new-availability.component.html',
    styleUrls: ['./new-availability.component.sass'],
})
export class NewAvailabilityComponent implements OnInit {
    @Input() showDeleteBlock: boolean = true;

    @Input() public slot?: IAvailabilitySlot;

    @Input() public title: string;

    @Output() saveChangesClick: EventEmitter<void> = new EventEmitter();

    @Output() cancelClick: EventEmitter<void> = new EventEmitter();

    @Output() deleteClick: EventEmitter<void> = new EventEmitter();

    @ViewChild(GeneralComponent) generalComponent: GeneralComponent;

    @ViewChild(EventDetailComponent) eventDetailComponent: EventDetailComponent;

    @ViewChild(ScheduleComponent) scheduleComponent: ScheduleComponent;

    public sideMenuGroups: SideMenuGroupTabs[];

    public isActive: boolean = true;

    public index: number = 0;

    // eslint-disable-next-line no-empty-function
    constructor(private router: Router) { }

    ngOnInit(): void {
        this.initializeSideMenu();
        this.isActive = this.slot?.isEnabled ?? true;
    }

    private initializeSideMenu() {
        this.sideMenuGroups = getNewAvailabilityMenu();
    }

    public goToPage(pageName: string) {
        this.router.navigate([`${pageName}`]);
    }

    enableSlot() {
        if (this.slot) {
            this.slot.isEnabled = !this.slot.isEnabled;
        }
    }

    public saveChanges() {
        if (this.index === 0 && this.generalComponent.generalForm.invalid) {
            return;
        }

        this.saveChangesClick.emit();
    }
}
