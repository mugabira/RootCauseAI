using RootCauseAI.Data;
using RootCauseAI.Engine;
using RootCauseAI.Models;
using Microsoft.AspNetCore.Mvc;
using RootCauseAI.Engine;
using RootCauseAI.Models;
using RootCauseAI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
builder.Services.AddSingleton<DiagnosticEngine>();

app.MapPost("/analyze/{tenantId}",
async (string tenantId, Ticket ticket, DiagnosticEngine engine) =>
{
    ticket.TenantId = tenantId;

    var result = engine.Analyze(ticket);

    TicketStore.Add(result);

    return Results.Ok(result);
});

app.MapGet("/tickets/{tenantId}",
(string tenantId) =>
{
    return TicketStore.GetByTenant(tenantId);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
