using ApiMvno.Services.Api;
using ApiMvno.Services.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureSerilog();
builder.UseStartup<Startup>();
