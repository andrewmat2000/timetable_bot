namespace TimeTableParser.Models;

internal class LessonModel {
  internal DateTime Start { get; set; }
  internal DateTime End { get; set; }
  internal int Number { get; set; }
  internal string Name { get; set; } = "";
  internal string TeachersName { get; set; } = "";
  internal string Address { get; set; } = "";
  internal string ClassRoom { get; set; } = "";
}