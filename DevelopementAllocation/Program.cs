using DevelopementAllocation.Repositories;
using DevelopementAllocation.Repository.FlightBooking;
using DevelopementAllocation.Data;
using DevelopementAllocation.Repository.ShuttleSlot;
using DevelopementAllocation.Services.Email;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<DevelopementAllocation.Repositories.TestGetApi.ITestRepository, TestRepository>();
builder.Services.AddScoped<IShuttleSlotRepository, ShuttleSlotRepository>();
// Add these two lines
builder.Services.AddScoped<IDbConnectionFactory, NpgsqlConnectionFactory>();
builder.Services.AddScoped<IFreightBookingRepository, FlightBookingRepository>();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200",
                "https://gentle-bay-05b3ef310.7.azurestaticapps.net"
              )
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.MapControllers();
app.Run();