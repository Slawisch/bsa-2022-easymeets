import { SideMenuGroup } from '@core/interfaces/sideMenu/sideMenuGroup';

export const getUserSettingsMenuItems = (): SideMenuGroup[] => [
    {
        header: 'Account',
        items: [
            { text: 'Personal', routerLink: 'account/personal' },
            { text: 'Login & Password', routerLink: 'default-route' },
            { text: 'Payments', routerLink: 'default-route' },
        ],
    },
    {
        header: 'Integrations',
        items: [
            { text: 'Calendars', routerLink: 'integrations/calendars' },
            { text: 'Video Conferencing', routerLink: 'integrations/video-conferencing' },
            { text: 'Mailing', routerLink: 'default-route' },
            { text: 'API & Tokens', routerLink: 'default-route' },
        ],
    },
];
