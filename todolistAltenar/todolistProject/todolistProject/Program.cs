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

//���� ���� ��� ����������
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<todolistDbContext>();

//    try
//    {
//        // ������� ���������� � ����
//        dbContext.Database.CanConnect(); // ��� await dbContext.Database.CanConnectAsync();
//        Console.WriteLine("����������� � ���� �������.");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine("������ ����������� � ����: " + ex.Message);
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
