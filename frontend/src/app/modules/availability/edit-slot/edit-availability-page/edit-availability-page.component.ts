import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IAvailabilitySlot } from '@core/models/IAvailiabilitySlot';
import { HttpInternalService } from '@core/services/http-internal.service';
import { SpinnerService } from '@core/services/spinner.service';
import { NewAvailabilityComponent } from '@modules/availability/new-slot/new-availability/new-availability.component';
import { Subject, takeUntil } from "rxjs";
import { AvailabilitySlotService } from "@core/services/availability-slot.service";
import { NotificationService } from "@core/services/notification.service";

@Component({
    selector: 'app-edit-availability-page',
    templateUrl: './edit-availability-page.component.html',
    styleUrls: ['./edit-availability-page.component.sass'],
})
export class EditAvailabilityPageComponent {
    private id: number | undefined;

    public slot?: IAvailabilitySlot;

    private unsubscribe$ = new Subject<void>();

    @ViewChild(NewAvailabilityComponent) newAvailabilityComponent: NewAvailabilityComponent;

    // eslint-disable-next-line no-empty-function
    constructor(
        private router: Router,
        private activateRoute: ActivatedRoute,
        private httpInternalService: HttpInternalService,
        private spinnerService: SpinnerService,
        private http: AvailabilitySlotService,
        private notifications: NotificationService
    ) {
        this.activateRoute.params.subscribe(params => {
            this.id = params['id'];
            this.spinnerService.show();
            this.httpInternalService
                .getRequest<IAvailabilitySlot>(`/availability/slot/${this.id}`)
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
        this.httpInternalService.putRequest<IAvailabilitySlot>(`/availability/${this.slot?.id}`, {
            generalDetailsUpdate: this.newAvailabilityComponent.generalComponent.settings,
            eventDetailsUpdate: this.newAvailabilityComponent.eventDetailComponent.settings,
            hasAdvancedSettings: this.newAvailabilityComponent.generalComponent.addAdvanced,
            isActive: this.newAvailabilityComponent.slot?.isEnabled ?? true
        }).subscribe(resp => {
            console.log('resp')
            console.log(resp)
            console.log(this.newAvailabilityComponent.generalComponent.settings)
        });
        this.goToPage('/availability');
    }

    public deleteSlot() {
        this.http
            .deleteSlot(this.slot?.id)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(
                () => {
                    this.notifications.showSuccessMessage('Slot was successfully deleted');
                    this.router.navigate(['/availability']);
                },
                (error) => {
                    this.notifications.showErrorMessage(error);
                },
            );
    }
}
