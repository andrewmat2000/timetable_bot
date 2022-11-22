namespace TimeTableParser.Parsers;

internal static class ParserBuilder {
  private static readonly Dictionary<string, IParser> _parsers = new();
  internal static string[] GetParsers() {
    var arr = new string[_parsers.Count];
    for (var i = 0; i < _parsers.Count; i++) {
      arr[i] = _parsers.ElementAt(i).Key;
    }
    return arr;
  }
  internal static void MapParser<T>() where T : IParser {
    IParser instance = Activator.CreateInstance<T>();
    _parsers.Add(instance.Name, instance);
  }
  internal static IParser? GetParser(string name) {
    return _parsers.ContainsKey(name) ? _parsers[name] : null;
  }

  static ParserBuilder() {
    ParserBuilder.MapParser<UrfuParser>();
  }
}