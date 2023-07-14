using todoapi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injection dependency
// builder.Services.AddScoped<IHelloWorldService, HelloWorldService>();
builder.Services.AddScoped<IUserService, UserService>();
// builder.Services.AddScoped<ITareaService, TareaService>();
// builder.Services.AddScoped<ICategoriaService, CategoriaService>();

// Database conexion with context
builder.Services.AddNpgsql<Context>(builder.Configuration.GetConnectionString("postgresql"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
