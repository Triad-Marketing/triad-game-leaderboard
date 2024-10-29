using Microsoft.EntityFrameworkCore;
using TriadLeaderboard;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ScoreDb>(opt => opt.UseInMemoryDatabase("ScoreBoard"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/scores", async (ScoreDb db) =>
    await db.Scores.ToListAsync());

app.MapPost("/scores", async (Score score, ScoreDb db) =>
{
    db.Scores.Add(score);
    await db.SaveChangesAsync();
    
    return Results.Created($"/scores/{score.Id}", score);
});

app.Run();