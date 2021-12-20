// This is an auto generated test

export interface LoggingController {

    Get(): LogEntry[];
}

export interface LogEntry {
    PostTime: DateTime;
    Body: string;
    Properties: Record<string, LogEventPropertyValue>;
}

export interface DateTime {
    Date: DateTime;
    Day: number;
    DayOfWeek: DayOfWeek;
    DayOfYear: number;
    Hour: number;
    Kind: DateTimeKind;
    Millisecond: number;
    Minute: number;
    Month: number;
    Now: DateTime;
    Second: number;
    Ticks: number;
    TimeOfDay: TimeSpan;
    Today: DateTime;
    Year: number;
    UtcNow: DateTime;
}

export interface LogEventPropertyValue {
}

export interface DayOfWeek {
}

export interface DateTimeKind {
}

export interface TimeSpan {
    Ticks: number;
    Days: number;
    Hours: number;
    Milliseconds: number;
    Minutes: number;
    Seconds: number;
    TotalDays: number;
    TotalHours: number;
    TotalMilliseconds: number;
    TotalMinutes: number;
    TotalSeconds: number;
}
