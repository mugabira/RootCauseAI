using Microsoft.AspNetCore.Mvc;
using RootCauseAI.Engine;
using RootCauseAI.Models;
using RootCauseAI.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<DiagnosticEngine>();
var app = builder.Build();

app.MapPost("/analyze", (Ticket ticket, DiagnosticEngine engine) =>
{
    var result = engine.Analyze(ticket);
    TicketStore.Add(result);
    return Results.Ok(result);
});

app.MapGet("/tickets", () => TicketStore.GetAll());

app.Run();
