import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { getNewAvailabilityMenu } from '@core/helpers/new-availability-menu-helper';
import { SideMenuGroup } from '@core/interfaces/sideMenu/sideMenuGroup';
import { ISlot } from '@core/interfaces/slot/slot-interface';
import { HttpInternalService } from '@core/services/http-internal.service';
import { SpinnerService } from '@core/services/spinner.service';
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-edit-availability-page',
    templateUrl: './edit-availability-page.component.html',
    styleUrls: ['./edit-availability-page.component.sass'],
})
export class EditAvailabilityPageComponent implements OnInit, AfterViewInit {
    public sideMenuGroups: SideMenuGroup[];

    public isActive: boolean = true;

    private id: number | undefined;

    private subscription: Subscription;

    constructor(
        private router: Router,
        private activateRoute: ActivatedRoute,
        private httpInternalService: HttpInternalService,
        private spinnerService: SpinnerService,
    ) { }

    ngOnInit(): void {
        this.initializeSideMenu();
    }

    private initializeSideMenu() {
        this.sideMenuGroups = getNewAvailabilityMenu();
    }

    public goToPage(pageName: string) {
        this.router.navigate([`${pageName}`]);
    }

    public ngAfterViewInit() {
        this.subscription = this.activateRoute.params.subscribe(params => {
            this.id = params['id'];
            this.spinnerService.show();
            this.httpInternalService
                .getRequest<ISlot>(`/availability/${this.id}`)
                .subscribe(slotResponse => {
                    console.log(slotResponse);
                    this.spinnerService.hide();
                });
        });
    }
}