using Microsoft.EntityFrameworkCore;
using todolist.Application.Services;
using todolistProject.dataAccess;
using todolistProject.dataAccess.CRUD;
using todolistProject.Core.Abstractions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<todolistDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("todolistDbContext"));
    });

builder.Services.AddScoped<INotesService, NotesService>();
builder.Services.AddScoped<INoteCRUD, NoteCRUD>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserCRUD, UserCRUD>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IGroupCRUD, GroupCRUD>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
