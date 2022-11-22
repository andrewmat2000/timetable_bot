using TimeTableParser.Models;

namespace TimeTableParser.Parsers;

internal interface IParser {
  internal string Name { get; }
  internal DayModel? GetDay(string groupName, DateTime day);
}