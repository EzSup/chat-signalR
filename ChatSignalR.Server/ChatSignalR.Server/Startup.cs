using ChatSignalR.Core.Interfaces.Repositories;
using ChatSignalR.Core.Interfaces.Services;
using ChatSignalR.Core.Services;
using ChatSignalR.DataAccess.AzureSQL;
using ChatSignalR.DataAccess.AzureSQL.Repositories;
using ChatSignalR.Server.Hubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ChatSignalR.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IMessageService, MessageService>();

            services.AddSingleton<ITextAnalyticsService, TextAnalyticsService>(provider =>
            {
                var endpoint = Configuration["Azure:Cognitive:ConnectionString"];
                var key = Configuration["Azure:Cognitive:key"];

                return new TextAnalyticsService(endpoint, key);
            });

            services.AddSignalR()
                .AddAzureSignalR(Configuration["Azure:SignalR:ConnectionString"]);

            services.AddDbContext<ChatDbContext>(
                options =>
                {
                    options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"], sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    });
                });

            services.AddCors(options =>
            {
                var allowedOrigins = Configuration.GetSection("AllowedClientsOrigins").Get<List<string>>();

                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(allowedOrigins.ToArray())
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
            });
        }
    }
}