using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestfulWebAPI.Helper;
using RestfulWebAPI.Models;
using RestfulWebAPI.Repository;
using RestfulWebAPI.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
   options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

 
builder.Services.AddTransient<IUnitofWork, UnitofWork>();
builder.Services.AddTransient<IModelMessage, ModelsMessage>();

builder.Services.AddCors(m =>
{
   m.AddPolicy("Powersoft", b =>
    b.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
   );
});
//to avoid MultiPartBodyLength 
builder.Services.Configure<FormOptions>(o =>
{
   o.ValueLengthLimit = int.MaxValue;
   o.MultipartBodyLengthLimit = int.MaxValue;
   o.MemoryBufferThreshold = int.MaxValue;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}
app.UseCors(op => {
   op.AllowAnyOrigin();
   op.AllowAnyHeader();
   op.AllowAnyMethod();
});
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
