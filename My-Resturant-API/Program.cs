using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using My_Resturant.Context;
using My_Resturant.Entities;
using My_Resturant.Helper;
using My_Resturant.Implementations;
using My_Resturant.Interfaces;
using System;
using System.Reflection;
using MailKit;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option=>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Resturant API",
        Version = "first version",
        Description = "A resturant website",
        Contact = new OpenApiContact
        {
            Name = "Omar Suliman",
            Email = "omarsit20004031@gmail.com",
            Url = new Uri("http://OmarSuliman.com/")
        }
    });
    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<RestDbContext>(con => con.UseSqlServer(
    "Data Source=ASUS-I3250\\SQLEXPRESS;Initial Catalog=ResturantDB;Integrated Security=True;Trust Server Certificate=True"));
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<AuthServices>();
builder.Services.AddScoped<ICategoryServises, CategoryServices>();
builder.Services.AddScoped<IIngrediantServices, IngrediantServces>();
builder.Services.AddScoped<IPersonServices, PersonServices>();
builder.Services.AddScoped<IItemServices, ItemServices>();
builder.Services.AddScoped<IMealServices, MealServices>();
builder.Services.AddScoped<IMealDetialsServices, MealDetialesServices>();
builder.Services.AddScoped<IReservationServices, ReservationServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<ICodeServices, CodeServices>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<TokenHelper>(provider =>
{
    var authServices = provider.GetRequiredService<AuthServices>();
    return new TokenHelper(authServices.IsTokenInBlackList);
});
builder.Services.AddScoped<SaveImages>();

//builder.Services.AddIdentity<Person, IdentityRole>().AddEntityFrameworkStores<RestDbContext>() //use this if you want a built in identity services
//    .AddDefaultTokenProviders();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();
app.UseStaticFiles();
var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsPath),
    RequestPath = "/Uploads"
});

// Configure the HTTP request pipeline.

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
