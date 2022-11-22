namespace TimeTableParser.Models;

internal struct TimetableModel {
  internal DayModel Monday { get; set; }
  internal DayModel Tuesday { get; set; }
  internal DayModel Wednesday { get; set; }
  internal DayModel Thursday { get; set; }
  internal DayModel Friday { get; set; }
  internal DayModel Saturday { get; set; }
  internal DayModel Sunday { get; set; }
}