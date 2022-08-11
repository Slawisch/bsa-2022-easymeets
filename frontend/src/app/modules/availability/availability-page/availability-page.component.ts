import { Component } from '@angular/core';
import { AvailabilitySlot } from '@core/models/availiability-slot';
import { TeamWithSlots } from '@core/models/team-with-slot';
import { User } from '@core/models/user';
import { AvailabilitySlotService } from '@core/services/availability-slot.service';
import { UserService } from '@core/services/user.service';

@Component({
    selector: 'app-availability-page',
    templateUrl: './availability-page.component.html',
    styleUrls: ['./availability-page.component.sass'],
})
export class AvailabilityPageComponent {
    public teams: Array<TeamWithSlots>;

    public currentUser: User;

    public userSlots: Array<AvailabilitySlot>;

    constructor(private availabilitySlotService: AvailabilitySlotService, private userService: UserService) {
        this.getCurrentUser();
    }

    public getAllAvailabilitySlotsForTeam() {
        this.availabilitySlotService
            .getAvailabilitySlotsGroupByTeams(this.currentUser.id)
            .subscribe(
                (resp) => {
                    this.teams = resp;
                },
            );
    }

    public getAllUsersAvailabilitySlots() {
        this.availabilitySlotService
            .getAllUsersAvailabilitySlots(this.currentUser.id)
            .subscribe(
                (resp) => {
                    this.userSlots = resp;
                },
            );
    }

    public getCurrentUser() {
        this.userService
            .getCurrentUserById(5)
            .subscribe(
                (resp) => {
                    if (resp) {
                        this.currentUser = resp;
                        this.getAllAvailabilitySlotsForTeam();
                        this.getAllUsersAvailabilitySlots();
                    }
                },
            );
    }
}
