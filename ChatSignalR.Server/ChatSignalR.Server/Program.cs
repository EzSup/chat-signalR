
using ChatSignalR.Core.Interfaces.Repositories;
using ChatSignalR.Core.Interfaces.Services;
using ChatSignalR.Core.Services;
using ChatSignalR.DataAccess.AzureSQL;
using ChatSignalR.DataAccess.AzureSQL.Repositories;
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
            //builder.Services.AddTransient<IChatRepository, ChatRepository>();
            //builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IMessageRepository, MessageRepository>();

            //builder.Services.AddTransient<ChatService>();
            //builder.Services.AddTransient<UserService>();
            builder.Services.AddSingleton<ITextAnalyticsService, TextAnalyticsService>(provider =>
            {
                var endpoint = builder.Configuration["Azure:Cognitive:ConnectionString"];
                var key = builder.Configuration["Azure:Cognitive:key"];

                return new TextAnalyticsService(endpoint, key);
            });
            builder.Services.AddTransient<IMessageService, MessageService>();

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
                    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"], sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5, 
                            maxRetryDelay: TimeSpan.FromSeconds(30), 
                            errorNumbersToAdd: null);
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

            app.UseAuthorization();

            app.MapHub<ChatHub>("/chat");

            app.MapControllers();

            app.Run();
        }
    }
}
