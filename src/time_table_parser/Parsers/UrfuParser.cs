using System.Text.RegularExpressions;

using HtmlAgilityPack;
using TimeTableParser.Models;

using HttpClient = Asd.HttpLib.HttpClient;

namespace TimeTableParser.Parsers;

internal class UrfuParser : IParser {
  private const string MainPage = "https://urfu.ru/";
  private static readonly Regex _dayRegex = new(@"<td class=""shedule-weekday-time"">.+<\/td>");

  string IParser.Name => "УрФу";

  private static string GenerateDateString(DateTime date) {
    return $"{date.Year}{(date.Month >= 10 ? $"{date.Month}" : $"0{date.Month}")}{(date.Day >= 10 ? $"{date.Day}" : $"0{date.Day}")}";
  }

  DayModel? IParser.GetDay(string groupName, DateTime date) {
    Asd.HttpLib.HttpClient client = new(MainPage);
    var answer = client.Get<string>($"api/schedule/groups/suggest/?query={groupName}").Value;

    var data = Regex.Match(answer, @"""data"": [0-9]+").Value;
    var number = int.Parse(Regex.Match(data, @"[0-9]+").Value);

    HtmlDocument htmlDocument = new();
    HtmlWeb web = new();
    htmlDocument = web.Load($"{MainPage}api/schedule/groups/lessons/{number}/{GenerateDateString(date)}/");

    if (htmlDocument.DocumentNode.SelectSingleNode("//table") is not HtmlNode table) {
      return null;
    }

    var tableArray = table.ChildNodes.ToArray();

    var reg = @"\s*<td colspan=""3""><b>{day} [а-я]+</b></td>".Replace("{day}", date.Day >= 10 ? $"{date.Day}" : $"0{date.Day}");

    for (var i = 0; i < tableArray.Length; i++) {
      if (tableArray[i].Name == "tr" && tableArray[i].HasClass("divide") && Regex.IsMatch(tableArray[i].InnerHtml, reg)) {

        return null;
      }
    }

    return null;
  }
}