import { Injectable } from '@angular/core';
import { INewUser } from '@core/models/INewUser';
import { IUser } from '@core/models/IUser';
import { map } from 'rxjs';

import { HttpInternalService } from './http-internal.service';

@Injectable({
    providedIn: 'root',
})
export class UserService {
    public routePrefix = '/user';

    // eslint-disable-next-line no-empty-function
    constructor(private httpService: HttpInternalService) {}

    public getCurrentUser() {
        return this.httpService.getRequest<IUser>(`${this.routePrefix}/current`).pipe(
            map((resp) => {
                this.setUser(resp);

                return resp;
            }),
        );
    }

    public editUser(put: IUser) {
        return this.httpService.putRequest<IUser>(`${this.routePrefix}`, put);
    }

    public createUser(user: INewUser) {
        return this.httpService.postRequest<IUser>(`${this.routePrefix}`, user);
    }

    private setUser(_user: IUser) {
        if (_user) {
            localStorage.setItem('user', JSON.stringify(_user));
        }
    }
}
