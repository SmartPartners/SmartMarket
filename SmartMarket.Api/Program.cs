
using SmartMarket.Data.DbContexts;
using Newtonsoft.Json;
using System;
using Microsoft.EntityFrameworkCore;
using SmartMarket.Service.Mappers;
using SmartMarket.Api.Extensions;
using SmartMarket.Service.Commons.Helpers;

namespace SmartMarket.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCustomServices();

            /// Fix the Cycle
            builder.Services.AddControllers()
                 .AddNewtonsoftJson(options =>
                 {
                     options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                 });

            //Set Database Configuration
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString")));

            builder.Services.AddAutoMapper(typeof(MapperProfile));

            var app = builder.Build();
            WebHostEnviromentHelper.WebRootPath = Path.GetFullPath("wwwroot");

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
        }
    }
}