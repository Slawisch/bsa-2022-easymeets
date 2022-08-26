import { Component, EventEmitter, OnInit } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseComponent } from '@core/base/base.component';
import { IImagePath } from '@core/models/IImagePath';
import { INewTeam } from '@core/models/INewTeam';
import { ITeam } from '@core/models/ITeam';
import { ConfirmationWindowService } from '@core/services/confirmation-window.service';
import { NotificationService } from '@core/services/notification.service';
import { TeamService } from '@core/services/team.service';
import { TimeZone } from '@shared/enums/timeZone';
import { map, Observable } from 'rxjs';

@Component({
    selector: 'app-team-preferences',
    templateUrl: './team-preferences.component.html',
    styleUrls: ['./team-preferences.component.sass'],
})
export class TeamPreferencesComponent extends BaseComponent implements OnInit {
    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private teamService: TeamService,
        public notificationService: NotificationService,
        private confirmationWindowService: ConfirmationWindowService,
    ) {
        super();
    }

    public clickEvent = new EventEmitter<void>();

    public isNewTeam: boolean = true;

    public imageUrl?: string;

    public team: ITeam;

    public formGroup: FormGroup;

    public timeZoneValues = Object.values(TimeZone);

    public nameControl: FormControl = new FormControl('', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50),
        Validators.pattern(/^[іІїЇaєЄa-zA-Z\dа-яА-Я-]+(\s|)[іІїЇєЄa-zA-Z\dа-яА-Я-]*$/),
    ]);

    public pageLinkControl: FormControl = new FormControl(
        '',
        [Validators.required],
        [this.teamLinkValidator()],
    );

    public descriptionControl: FormControl = new FormControl(
        '',
        [
            Validators.required,
            Validators.minLength(2),
            Validators.maxLength(50),
            Validators.pattern(/^[.,іІїЇaєЄa-zA-Z\dа-яА-Я-\s]*$/)],
    );

    public ngOnInit(): void {
        this.formGroup = new FormGroup({
            name: this.nameControl,
            logo: new FormControl(),
            pageLink: this.pageLinkControl,
            timeZone: new FormControl(),
            description: this.descriptionControl,
        });
    }

    private getTeam(teamId: number) {
        this.teamService.getTeamById(teamId)
            .pipe(this.untilThis)
            .subscribe((team) => {
                this.team = team;
                this.formGroup.patchValue({
                    name: team.name,
                    image: team.image,
                    pageLink: team.pageLink,
                    timeZone: team.timeZone,
                    description: team.description,
                });
                this.isNewTeam = false;
            });
    }

    public deleteTeam() {
        this.teamService
            .deleteTeam(this.team.id)
            .pipe(this.untilThis)
            .subscribe(
                () => {
                    this.router.navigate(['../'], { relativeTo: this.route });
                    this.notificationService.showSuccessMessage('Team information was deleted successfully.');
                },
                () => {
                    this.notificationService.showErrorMessage('There was an error while deleting.');
                },
            );
    }

    public OnSubmit(form: FormGroup) {
        if (this.isNewTeam) {
            this.createTeam(form);
        } else {
            this.editeTeam(form);
        }
    }

    public generateNewPageLink(formGroup: FormGroup) {
        if (this.isNewTeam) {
            if (formGroup.value.name.length > 0) {
                this.getUniquePageLink(formGroup.value.name);
            } else {
                this.formGroup.patchValue({
                    pageLink: '',
                });
            }
        }
    }

    public loadImage(event: Event) {
        const target = event.target as HTMLInputElement;
        const fileToUpload: File = (target.files as FileList)[0];

        if (fileToUpload.size / 1000000 > 5) {
            this.confirmCancelDialog();

            return;
        }
        const formData = new FormData();

        formData.append('file', fileToUpload, fileToUpload.name);
        this.uploadLogo(formData);
    }

    public confirmCancelDialog(): void {
        this.confirmationWindowService.openConfirmDialog({
            buttonsOptions: [
                {
                    class: 'confirm-accept-button',
                    label: 'Ok',
                    onClickEvent: this.clickEvent,
                },
            ],
            title: 'Oops...',
            message: "Image can't be heavier than 5MB!",
        });
    }

    private uploadLogo(formData: FormData) {
        this.teamService
            .uploadLogo(formData, this.team.id)
            .pipe(this.untilThis)
            .subscribe(
                (resp: IImagePath) => {
                    this.imageUrl = resp.path;
                },
                () => {
                    this.notificationService.showErrorMessage('Something went wrong. Picture was not uploaded.');
                },
            );
    }

    private getUniquePageLink(teamName: string) {
        this.teamService
            .getNewPageLink(this.team ? this.team.id : 0, teamName.replace(/\s/g, ''))
            .pipe(this.untilThis)
            .subscribe((res) => {
                this.formGroup.patchValue({
                    pageLink: res,
                });
            });
    }

    private createTeam(form: FormGroup) {
        const newTeam: INewTeam = {
            image: 'this.team.image',
            name: form.value.name,
            pageLink: form.value.pageLink,
            timeZone: form.value.timeZone,
            description: form.value.description,
        };

        this.teamService
            .createTeam(newTeam)
            .pipe(this.untilThis)
            .subscribe(
                (team) => {
                    this.team = team;
                    this.formGroup.patchValue({
                        name: team.name,
                        image: team.image,
                        pageLink: team.pageLink,
                        timeZone: team.timeZone,
                        description: team.description,
                    });
                    this.isNewTeam = false;
                    this.notificationService.showSuccessMessage('Team information was created successfully.');
                },
                () => {
                    this.notificationService.showErrorMessage('There was an error while creating.');
                },
            );
    }

    private editeTeam(form: FormGroup) {
        const editedTeam: ITeam = {
            id: this.team.id,
            image: this.team.image,
            name: form.value.name,
            pageLink: form.value.pageLink,
            timeZone: form.value.timeZone,
            description: form.value.description,
        };

        this.teamService
            .editTeam(editedTeam)
            .pipe(this.untilThis)
            .subscribe(
                () => {
                    this.isNewTeam = false;
                    this.notificationService.showSuccessMessage('Team information was updated successfully.');
                },
                () => {
                    this.notificationService.showErrorMessage('There was an error while updating.');
                },
            );
    }

    private teamLinkValidator(): AsyncValidatorFn {
        return (control: AbstractControl): Observable<ValidationErrors | null> =>
            this.validateTeamLink(control.value)
                .pipe(this.untilThis)
                .pipe(
                    map((responce) => (responce ? null : { teamLinkUniq: true })),
                );
    }

    private validateTeamLink(teamlink: string): Observable<boolean> {
        return this.teamService.validatePageLink(this.team ? this.team.id : 0, teamlink);
    }
}
