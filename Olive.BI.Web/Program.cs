using NewLife.Log;
using Rock.Common;

XTrace.UseConsole();
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;
builder.Services.AddServiceInit(config);//初始化
var app = builder.Build();
app.AddAppInit(config);

// SPA fallback：未匹配 API 与静态资源时，返回 index.html，支持 Vue history 模式
app.MapFallbackToFile("index.html");

app.Run();