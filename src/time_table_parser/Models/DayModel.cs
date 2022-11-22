namespace TimeTableParser.Models;

internal struct DayModel {
  internal DateTime Date { get; set; }
  internal LessonModel[] LessonModels { get; set; }
}