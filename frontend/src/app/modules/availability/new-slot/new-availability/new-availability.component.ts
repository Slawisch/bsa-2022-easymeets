import { Component, OnInit } from '@angular/core';
import { SideMenuGroup } from '@core/interfaces/sideMenu/sideMenuGroup';

@Component({
    selector: 'app-new-availability',
    templateUrl: './new-availability.component.html',
    styleUrls: ['./new-availability.component.sass'],
})
export class NewAvailabilityComponent implements OnInit {
    public sideMenuGroup: SideMenuGroup;

    ngOnInit(): void {
        this.initializeSideMenu();
    }

    private initializeSideMenu() {
        this.sideMenuGroup = {
            items: [
                { text: 'General', routerLink: 'general' },
                { text: 'Booking page', routerLink: 'event-details' },
            ],
        };
    }
}
