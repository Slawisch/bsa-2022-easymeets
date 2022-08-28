import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnInit } from '@angular/core';
import { IScheduleItem } from '@core/models/schedule/IScheduleItem';
import { CalendarEvent, CalendarEventTimesChangedEvent, CalendarView } from 'angular-calendar';
import { addHours, addMinutes, isSameDay, setDay, startOfDay } from 'date-fns';
import { Subject } from 'rxjs';

@Component({
    selector: 'app-schedule-week',
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './schedule-week.component.html',
    styleUrls: ['./schedule-week.component.sass'],
})
export class ScheduleWeekComponent implements OnInit {
    @Input() public items: IScheduleItem[];

    @Input() public itemChange: EventEmitter<void> = new EventEmitter();

    public view: CalendarView = CalendarView.Week;

    public viewDate: Date = new Date();

    public events: CalendarEvent[];

    public refresh = new Subject<void>();

    public ngOnInit(): void {
        this.updateEvents();

        this.itemChange.subscribe(() => {
            this.updateEvents();
        });
    }

    public eventTimesChanged({
        event,
        newStart,
        newEnd,
    }: CalendarEventTimesChangedEvent): void {
        event.start = newStart;
        event.end = newEnd;
        const index = this.events.indexOf(event);

        this.items[index].start = `${this.reformat(newStart.getHours())}:${this.reformat(newStart.getMinutes())}:00`;
        if (newEnd) {
            this.items[index].end = `${this.reformat(newEnd.getHours())}:${this.reformat(newEnd.getMinutes())}:00`;
        }
        this.itemChange.emit();
        this.refresh.next();
    }

    public validateEventTimesChanged = (
        { event, newStart, newEnd }: CalendarEventTimesChangedEvent,
    ) => {
        if (newEnd) {
            delete event.cssClass;
            const sameDay = isSameDay(newStart, newEnd);

            if (!sameDay) {
                return false;
            }
        }

        return true;
    };

    private updateEvents() {
        let events: CalendarEvent[] = [];

        this.items.forEach((item) => {
            if (item.isEnabled) {
                events = events.concat({
                    start: addMinutes(addHours(
                        startOfDay(setDay(new Date(), item.weekDay)),
                        this.parseTime(item.start).getHours(),
                    ), this.parseTime(item.start).getMinutes()),
                    end: addMinutes(addHours(
                        startOfDay(setDay(new Date(), item.weekDay)),
                        this.parseTime(item.end).getHours(),
                    ), this.parseTime(item.end).getMinutes()),
                    title: '',
                    resizable: {
                        beforeStart: true,
                        afterEnd: true,
                    },
                });
            }
        });

        this.events = events;
        this.refresh.next();
    }

    private reformat(num: number): string {
        if (num < 10) {
            return `0${num}`;
        }

        return num.toString();
    }

    private parseTime(time: string): Date {
        const d = new Date();
        const [hours, minutes, seconds] = time.split(':');

        d.setHours(+hours);
        d.setMinutes(+minutes);
        d.setSeconds(+seconds);

        return d;
    }
}
