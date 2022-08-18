import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IAvailabilitySlot } from '@core/models/IAvailiabilitySlot';
import { IUpdateAvailability } from '@core/models/IUpdateAvailability';
import { AvailabilitySlotService } from '@core/services/availability-slot.service';
import { NotificationService } from '@core/services/notification.service';
import { SpinnerService } from '@core/services/spinner.service';
import { NewAvailabilityComponent } from '@modules/availability/new-slot/new-availability/new-availability.component';
import { Subject, takeUntil } from 'rxjs';

@Component({
    selector: 'app-edit-availability-page',
    templateUrl: './edit-availability-page.component.html',
    styleUrls: ['./edit-availability-page.component.sass'],
})
export class EditAvailabilityPageComponent {
    private id: bigint | undefined;

    public slot?: IAvailabilitySlot;

    private unsubscribe$ = new Subject<void>();

    @ViewChild(NewAvailabilityComponent) newAvailabilityComponent: NewAvailabilityComponent;

    // eslint-disable-next-line no-empty-function
    constructor(
        private router: Router,
        private activateRoute: ActivatedRoute,
        private spinnerService: SpinnerService,
        private http: AvailabilitySlotService,
        private notifications: NotificationService,
    ) {
        this.activateRoute.params.subscribe(params => {
            this.id = params['id'];
            this.spinnerService.show();
            this.http.getSlotById(this.id)
                .subscribe(slotResponse => {
                    this.slot = slotResponse;
                    this.spinnerService.hide();
                });
        });
    }

    public goToPage(pageName: string) {
        this.router.navigate([`${pageName}`]);
    }

    sendChanges() {
        const updateAvailability: IUpdateAvailability = {
            generalDetailsUpdate: this.newAvailabilityComponent.generalComponent.settings,
            eventDetailsUpdate: this.newAvailabilityComponent.eventDetailComponent.settings,
            hasAdvancedSettings: this.newAvailabilityComponent.generalComponent.addAdvanced,
            schedule: this.newAvailabilityComponent.scheduleComponent.schedule,
            isActive: this.newAvailabilityComponent.slot?.isEnabled ?? true,
        };

        this.http.updateSlot(updateAvailability, this.slot?.id)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(
                () => {
                    this.notifications.showSuccessMessage('Slot was successfully updated');
                    this.goToPage('/availability');
                },
                (error) => {
                    this.notifications.showErrorMessage(error);
                },
            );
    }

    public deleteSlot() {
        this.http
            .deleteSlot(this.slot?.id)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(
                () => {
                    this.notifications.showSuccessMessage('Slot was successfully deleted');
                    this.goToPage('/availability');
                },
                (error) => {
                    this.notifications.showErrorMessage(error);
                },
            );
    }
}
