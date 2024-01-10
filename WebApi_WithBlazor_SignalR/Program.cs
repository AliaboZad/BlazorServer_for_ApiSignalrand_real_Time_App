using Microsoft.EntityFrameworkCore;
using WebApi_WithBlazor_SignalR.DBFolder;
using WebApi_WithBlazor_SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

/////////////////// Add DbContext /////////////////


builder.Services.AddDbContext<AppDbContext>(option => option.UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("Myconnect")));


/////////////////SignalR /////////////////////
builder.Services.AddSignalR( option => { option.EnableDetailedErrors = true;  });

//////////////////////////////////  CROS  /////////////////////////////////////
builder.Services.AddCors(cors =>
{
    cors.AddPolicy("MyPolicy", corsPolicyBuilder =>
    {
        corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("MyPolicy");

//  app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.MapControllers();
app.MapHub<SignalHub>("/signalhub");

app.Run();
