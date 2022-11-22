using Asd.HttpLib.AspNet;
using TimeTableParser;
using TimeTableParser.Parsers;

const string Group = "РИМ-120990";

ParserBuilder.MapParser<UrfuParser>();
IParser parser = ParserBuilder.GetParser("УрФу");

var timetable = parser.GetDay(Group, DateTime.Now.AddDays(2));



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAsdHttpServer();
var app = builder.Build();
app.UseAsdHttpService<ParserServer>();

app.Run();
