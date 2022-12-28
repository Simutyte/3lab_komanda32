﻿using _3lab_komanda32.Repositories;
using Microsoft.EntityFrameworkCore;

namespace _3lab_komanda32
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApiDbContext>(o => o.UseSqlite("Data source = DB/ApiDb.db"));

            //repositories
            builder.Services.AddScoped<BusinessRepository>();
            builder.Services.AddScoped<OrderRepository>();
            builder.Services.AddScoped<PaymentRepository>();
            builder.Services.AddScoped<ProductRepository>();
            builder.Services.AddScoped<ReservationRepository>();
            builder.Services.AddScoped<ServiceRepository>();
            builder.Services.AddScoped<LoyaltyRepository>();



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
        }
    }
}
