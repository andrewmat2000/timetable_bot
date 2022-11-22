using TimeTableParser.Models;

using HttpClient = Asd.HttpLib.HttpClient;
using HttpMethod = Asd.HttpLib.HttpMethod;

namespace TimeTableParser;

internal class ParserServer {
  [HttpMethod("get_timetable")]
  internal TimetableModel? GetTimetable() {
    return null;
  }
}