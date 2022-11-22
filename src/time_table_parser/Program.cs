using Asd.HttpLib.AspNet;
using TimeTableParser;
using TimeTableParser.Parsers;

const string Group = "РИМ-120990";

IParser parser = ParserBuilder.GetParser("УрФу");

var timetable = parser.GetDay(Group, DateTime.Now);



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAsdHttpServer();
var app = builder.Build();
app.UseAsdHttpService<ParserServer>();

// app.Run();
