
using ASP.NET_web_api_learning.models.ProjectModels;
using ASP.NET_web_api_learning.Services;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_web_api_learning
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<HashingService>();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowSpecificOrigin",
                    policy => policy.WithOrigins("https://localhost/")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            { 
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
            }
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowSpecificOrigin");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
