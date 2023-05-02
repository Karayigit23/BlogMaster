
using BlogMaster.Core.Command.ArticleCommand;
using BlogMaster.Core.InterFaces;
using BlogMaster.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IArticleVoteRepository, ArticleVoteRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddMediatR(typeof(CreateArticleCommand));
builder.Services.AddSwaggerDocument(p=>p.PostProcess=(o => { o.Info.Title = "BlogMaster";}));

builder.Services.AddDbContext<DbContext>(p => p.UseSqlServer(builder.Configuration.GetValue<string>("sqlConnection")));


builder.Services.AddControllers();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var context = serviceProvider.GetService<DbContext>();
    context.Database.EnsureCreated();
    context.Database.Migrate();
}


app.UseAuthorization();
app.MapControllers();
app.UseOpenApi();
app.UseSwaggerUi3();

app.MapGet("/", () => "Hello World!");

app.Run();
