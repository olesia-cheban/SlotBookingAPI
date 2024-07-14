using SlotBooking.Data;
using SlotBooking.Domain;

namespace SlotBooking.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHttpClient();
        builder.Services.AddControllers();
        builder.Services.AddScoped<ISlotRepository, SlotRepository>();
        builder.Services.AddScoped<ISlotService, SlotService>();
        builder.Services.AddRouting();

        builder.Services.Configure<SlotServiceOptions>(
            builder.Configuration.GetSection(SlotServiceOptions.SlotService));
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<ErrorHandlerMiddleware>();
        
        app.MapControllers();
        app.UseAuthorization();

        app.Run();
    }
}