using Microsoft.EntityFrameworkCore;
using AgendamentoApi.Services;

var builder = WebApplication.CreateBuilder(args);

//Config do DB
builder.Services.AddDbContext<AgendamentoApi.AppDbContext>(options =>
    options.UseSqlite("Data Source=agendamento.db"));


//Configurando o CORS 

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add services to the container.

builder.Services.AddScoped<IPedidoService, PedidoService>();

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

//Ativando o CORS
app.UseCors("PermitirTudo");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
