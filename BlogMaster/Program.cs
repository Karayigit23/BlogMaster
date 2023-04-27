using BlogMaster.Core.Command.ArticleCommand;
using BlogMaster.Core.InterFaces;
using BlogMaster.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IArticleVoteRepository, ArticleVoteRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddMediatR(typeof(CreateArticleCommand));
builder.Services.AddSwaggerDocument(p=>p.PostProcess=(o => { o.Info.Title = "BlogMaster";}));
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<DbContext>();
    context.Database.EnsureCreated();
    context.Database.Migrate();
}
builder.Services.AddDbContext<DbContext>(p => p.UseSqlServer(builder.Configuration.GetValue<string>("sqlConnection")));

app.UseAuthorization();
app.MapControllers();
app.UseOpenApi();
app.UseSwaggerUi3();

app.MapGet("/", () => "Hello World!");

app.Run();