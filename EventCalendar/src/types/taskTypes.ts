export type DayId = 'monday' | 'tuesday' | 'wednesday' | 'thursday' ;

export interface Task {
  id: string;
  content: string;
  dayId: DayId;
}

export interface Day {
  id: DayId;
  title: string;
}