using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using todolist.Application.Services;
using todolistProject.dataAccess;
using todolistProject.dataAccess.CRUD;

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

var app = builder.Build();

//Блок кода для тестировки
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<todolistDbContext>();

//    try
//    {
//        // Пробуем обратиться к базе
//        dbContext.Database.CanConnect(); // или await dbContext.Database.CanConnectAsync();
//        Console.WriteLine("Подключение к базе успешно.");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine("Ошибка подключения к базе: " + ex.Message);
//    }
//}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
