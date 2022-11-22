using System.Text.RegularExpressions;

using TimeTableParser.Models;

namespace TimeTableParser.Parsers;

internal class UrfuParser : IParser {
  private readonly Regex TimeRegex = new(@"<td class=""shedule-weekday-time"">.+<\/td>");
  private const string MainPage = "https://urfu.ru/";

  string IParser.Name => "УрФу";
  // 20220921

  private static string GenerateDateString(DateTime date) {
    return $"{date.Year}{(date.Month >= 10 ? $"{date.Month}" : $"0{date.Month}")}{(date.Day >= 10 ? $"{date.Day}" : $"0{date.Day}")}";
  }

  DayModel? IParser.GetDay(string groupName, DateTime date) {
    Asd.HttpLib.HttpClient client = new("https://urfu.ru/");
    var answer = client.Get<string>($"api/schedule/groups/suggest/?query={groupName}").Value;

    var data = Regex.Match(answer, @"""data"": [0-9]+").Value;
    var number = int.Parse(Regex.Match(data, @"[0-9]+").Value);

    var timetable = client.Get<string>($"api/schedule/groups/lessons/{number}/{GenerateDateString(date)}/", new Dictionary<string, string> { { "Accept-Language", "ru" } });
    var days = timetable.Value;
    // var days = Regex.Match(timetable, @"<tr class=""shedule-weekday-row shedule-weekday-first-row"">.*</tr>", RegexOptions.Singleline).Value;
    Models.DayModel dayModel = new();
    bool isCurrentDay = false;
    var reg = @"\s*<td colspan=""3""><b>{day} [а-я]+</b></td>".Replace("{day}", date.Day >= 10 ? $"{date.Day}" : $"0{date.Day}");
    var lessons = new List<LessonModel>();
    var arr = Regex.Replace(days, @"^\s+", "", RegexOptions.Multiline).Split('\n', StringSplitOptions.RemoveEmptyEntries)[0..40];
    foreach (var a in arr) {
      Console.WriteLine(a);
    }
    for (var i = 0; i < arr.Length; i++) {
      if (Regex.IsMatch(arr[i], reg)) {
        if (isCurrentDay) {
          break;
        }
        isCurrentDay = true;
        continue;
      }

      if (isCurrentDay && TimeRegex.IsMatch(arr[i])) {

        continue;
      }
    }



    // var timetable = Regex.Matches(timetable, @"a");

    //Console.WriteLine(day);

    return dayModel;
  }
}