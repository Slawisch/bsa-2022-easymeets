import { Component, Input, OnInit } from '@angular/core';
import { AvailabilitySlot } from '@core/models/availiability-slot';
import { TeamWithSlot } from '@core/models/team-with-slot';

@Component({
    selector: 'app-team',
    templateUrl: './team.component.html',
    styleUrls: ['./team.component.sass'],
})
export class TeamComponent implements OnInit {
    @Input() public team: TeamWithSlot;

    public slots: Array<AvailabilitySlot>;

    ngOnInit(): void {
        this.slots = this.team.availabilitySlots;
    }
}
