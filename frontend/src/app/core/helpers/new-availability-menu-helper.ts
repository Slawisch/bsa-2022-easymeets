import { SideMenuGroupTabs } from '@core/interfaces/sideMenu/tabs/sideMenuGroupTabs';

export const getNewAvailabilityMenu = (): SideMenuGroupTabs[] => [
    {
        items: [
            { text: 'General' },
            { text: 'Booking page' },
            { text: 'Questions' },
            { text: 'Schedule' },
            { text: 'Notification Emails' },
        ],
    },
];
