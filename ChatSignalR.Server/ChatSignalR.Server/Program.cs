
using ChatSignalR.DataAccess.AzureSQL;
using ChatSignalR.Server.Hubs;
using Microsoft.EntityFrameworkCore;

namespace ChatSignalR.Server
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
            builder.Services.AddSignalR()
                .AddAzureSignalR();

            var dbcon = builder.Configuration["ConnectionStrings:DefaultConnection"];

            builder.Services.AddDbContext<ChatDbContext>(
                options =>
                {
                    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapHub<ChatHub>("/chat");

            app.MapControllers();

            app.Run();
        }
    }
}
